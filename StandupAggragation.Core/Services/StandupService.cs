using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HipchatApiV2;
using HipchatApiV2.Responses;
using ImpromptuInterface;
using ServiceStack;
using StandupAggragation.Core.Models;

namespace StandupAggragation.Core.Services
{
    public class IntegrationCredential
    {
        public string AuthToken { get; set; }
    }

    public class StandupService : IStandupService
    {
        private HipchatClient _client;
        private IntegrationCredential _credential;

        public StandupService(string authToken)
        {
            Init(authToken);
        }

        public HipchatViewRoomHistoryResponse GetMessageHistory(string roomName, string botName, DateTime date)
        {
            var results = _client.ViewRoomHistory("Rave Scrum and Announcements", date.ToLocalTime().ToString(), "EST",
                0, 1000);
            return results;
        }

        public IList<IStandupMessage> GetAllStandupHistory(string roomName, string botName)
        {
            
            var earliest = DateTime.Now;
            var items = new List<HipchatViewRoomHistoryResponseItems>();
            while (true)
            {
                var oneFetch = GetMessageHistory(roomName, botName, earliest);
                items.AddRange(oneFetch.Items);
                if (oneFetch.Items.Count < 1000)
                {
                    break;
                }
                var earliestForOneFetch = oneFetch.Items.First().Date;
                earliest = earliestForOneFetch < earliest ? earliestForOneFetch : earliest;
            }

            var standupReg = new Regex(@"/standup .*");
            var standups =
                items.Where(o => o.From != "Standup" && standupReg.IsMatch(o.Message)).Select(Convert).ToList();

            //var latest = standups.GroupBy(o => o.Date.Date).ToDictionary(o => o.Key, o => o.GroupBy(p=>p.From).ToDictionary(w=>w.Key, q=>q.OrderBy(r=>r.Date).Last()));
            //{id: 101942, links: {self: https://api.hipchat.com/v2/user/101942}, mention_name: JunShao, name: Jun Shao, version: 00000000}
            return standups;
        }


        public void Init(string authToken)
        {
            _credential = new IntegrationCredential {AuthToken = authToken};
            _client = new HipchatClient(authToken);
        }

        private IStandupMessage Convert(HipchatViewRoomHistoryResponseItems item)
        {
            var regex = new Regex(@"\bid: (\d{0,20})\b");
            var userId = regex.Match(item.From).Groups[1].Value;

            regex = new Regex(@"\bname: \b(.*)\b,");
            var username = regex.Match(item.From).Groups[1].Value;
            //Match tags
            regex = new Regex(@"\[([^\[\]]*)");
            var message = item.Message.TrimPrefixes("/standup ");
            var date = item.Date;
            var tags = regex.Match(item.Message).Groups.Cast<Group>().Skip(1).Select(o => o.Value).ToList();
            return
                new {UserId = userId, UserName = username, Message = message, Date = date, Tags = tags}
                    .ActLike<IStandupMessage>();
        }
    }
}
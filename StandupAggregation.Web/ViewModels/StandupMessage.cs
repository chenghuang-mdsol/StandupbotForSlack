using System;
using System.Linq;
using System.Reflection;
using Mvc.JQuery.DataTables;
using Mvc.JQuery.DataTables.Models;
using Newtonsoft.Json.Linq;
using StandupAggragation.Core.Models;

namespace StandupAggregation.Web.ViewModels
{
    public class StandupMessage
    {
        public StandupMessage(IStandupMessage msg)
        {
            UserId = msg.UserId;
            UserName = msg.UserName;
            Message = msg.Message;
            Date = msg.Date;
            Tags = msg.Tags != null && msg.Tags.Any() ? "[" + string.Join("][", msg.Tags) + "]" : "";
        }

        //public static StandupMessage CopyFrom(IStandupMessage message)
        //{
        //    StandupMessage result = new StandupMessage();
        //    DynamicCopy.CopyProperties(message, result);
        //    return result;
        //}


        public string UserId { get; set; }

        [DataTables(Searchable = true)]
        public string UserName { get; set; }

        [DataTables(Searchable = true)]
        public string Message { get; set; }

        [DataTables(SortDirection = SortDirection.Ascending, Order = 0)]
        //[DefaultToStartOf1970]
        public DateTime Date { get; set; }

        [DataTables(Searchable = true)]
        public string Tags { get; set; }
    }

    public class DefaultToStartOf1970Attribute : DataTablesAttributeBase
    {
        public override void ApplyTo(ColDef colDef, PropertyInfo pi)
        {
            colDef.SearchCols = colDef.SearchCols ?? new JObject();
            colDef.SearchCols["sSearch"] = new DateTime(1970, 1, 1).ToString("g") + "~" +
                                           DateTimeOffset.Now.Date.AddDays(1).ToString("g");
        }
    }
}
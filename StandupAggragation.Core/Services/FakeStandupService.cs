using System;
using System.Collections.Generic;
using HipchatApiV2.Responses;
using StandupAggragation.Core.DataAccess;
using StandupAggragation.Core.Models;

namespace StandupAggragation.Core.Services
{
    public class FakeStandupService : IStandupService
    {
        private readonly BaseRepository<IStandupMessage> _repository;

        public FakeStandupService()
        {
            _repository = new FakeStandupMessageRepository();
        }

        public HipchatViewRoomHistoryResponse GetMessageHistory(string roomName, string botName, DateTime date)
        {
            throw new NotImplementedException();
        }

        public IList<IStandupMessage> GetAllStandupHistory(string roomName, string botName)
        {
            var result = _repository.GetItems(new {GetAll = true});
            return result.Succeed ? result.Result : null;
        }
    }
}
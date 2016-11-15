using System;
using System.Collections.Generic;
using HipchatApiV2.Responses;
using StandupAggragation.Core.Models;

namespace StandupAggragation.Core.Services
{
    public interface IStandupService
    {
        HipchatViewRoomHistoryResponse GetMessageHistory(string roomName, string botName, DateTime date);
        IList<IStandupMessage> GetAllStandupHistory(string roomName, string botName);
    }
}
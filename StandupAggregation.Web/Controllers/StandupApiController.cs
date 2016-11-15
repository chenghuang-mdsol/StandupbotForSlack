using System.Linq;
using System.Web.Http;
using Mvc.JQuery.DataTables;
using Mvc.JQuery.DataTables.Models;
using StandupAggragation.Core.Services;
using StandupAggregation.Web.ViewModels;

namespace StandupAggregation.Web.Controllers
{
    public class StandupApiController : ApiController
    {
        public DataTablesResult<StandupMessage> GetAll(DataTablesParam dataTableParam)
        {
            var service = new StandupService("rWhFopVMgXRBxHUQIzqvDlMHOLuYA5obelp3SOVx");
            var history = service.GetAllStandupHistory("Rave Scrum and Announcements", "Standup");
            var result = DataTablesResult.Create(history.Select(o => new StandupMessage(o)).AsQueryable(),
                dataTableParam, ArrayOutputType.ArrayOfObjects);
            return result;
        }
    }
}
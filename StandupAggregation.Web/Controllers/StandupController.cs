using System;
using System.Linq;
using System.Web.Mvc;
using Mvc.JQuery.DataTables;
using Mvc.JQuery.DataTables.Models;
using QueryInterceptor;
using StandupAggragation.Core.Services;
using StandupAggregation.Web.Helpers;
using StandupAggregation.Web.ViewModels;

namespace StandupAggregation.Web.Controllers
{
    public class StandupController : Controller
    {
        // GET: Standup
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAll()
        {
            return View();
        }

        public DataTablesResult<StandupMessage> GetAll(DataTablesParam dataTableParam)
        {
            var service = new StandupService("rWhFopVMgXRBxHUQIzqvDlMHOLuYA5obelp3SOVx");
            //IStandupService service = new FakeStandupService();
            var history = service.GetAllStandupHistory("Rave Scrum and Announcements", "Standup");
            var result =
                DataTablesResult.Create(
                    history.Select(o => new StandupMessage(o))
                        .AsQueryable()
                        .InterceptWith(new SetComparerExpressionVisitor(StringComparison.CurrentCultureIgnoreCase))
                    , dataTableParam, (ArrayOutputType?) null);
            return result;
        }
    }
}
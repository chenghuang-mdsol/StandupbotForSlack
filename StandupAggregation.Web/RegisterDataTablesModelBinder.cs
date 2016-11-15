using System.Web;
using System.Web.Mvc;
using Mvc.JQuery.DataTables;
using StandupAggregation.Web;

[assembly: PreApplicationStartMethod(typeof (RegisterDataTablesModelBinder), "Start")]

namespace StandupAggregation.Web
{
    public static class RegisterDataTablesModelBinder
    {
        public static void Start()
        {
            if (!ModelBinders.Binders.ContainsKey(typeof (DataTablesParam)))
                ModelBinders.Binders.Add(typeof (DataTablesParam), new DataTablesModelBinder());
        }
    }
}
using Microsoft.Owin;
using Owin;
using StandupAggregation.Web;

[assembly: OwinStartup(typeof (Startup))]

namespace StandupAggregation.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BullsAndCows.Web.Startup))]

namespace BullsAndCows.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

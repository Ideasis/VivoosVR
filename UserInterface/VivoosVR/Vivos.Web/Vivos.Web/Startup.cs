using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VivoosVR.Web.Startup))]
namespace VivoosVR.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

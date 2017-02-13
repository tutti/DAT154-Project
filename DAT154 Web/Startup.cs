using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DAT154_Web.Startup))]
namespace DAT154_Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ecom.Web.Startup))]
namespace Ecom.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

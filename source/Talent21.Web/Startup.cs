using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Talent21.Web.Startup))]
namespace Talent21.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

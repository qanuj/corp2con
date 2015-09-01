using Microsoft.Owin;
using Owin;
using Talent21.Web.Areas.HelpPage;

[assembly: OwinStartupAttribute(typeof(Talent21.Web.Startup))]
namespace Talent21.Web
{
    public class Product
    {
        public static string Name { get { return "Contractor's Poll"; } }
    }
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            IocHelper.CreateContainer(app);
        }
    }
}

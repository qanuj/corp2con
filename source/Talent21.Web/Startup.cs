using Microsoft.Owin;
using Owin;
using Talent21.Web.Areas.HelpPage;

[assembly: OwinStartupAttribute(typeof(Talent21.Web.Startup))]
namespace Talent21.Web
{
    public class Product
    {
        public static string Name => "Contractor's Pool";
        public static string Slogan => "Connect More";
        public static string Address => "Hyderabad, India";
        public static string Color => "#3bca96";
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

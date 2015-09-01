using Talent21.Web;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(ElmahInitializer), "Initialize")]

namespace Talent21.Web
{
    using Elmah.SqlServer.EFInitializer;

    public static class ElmahInitializer
    {
        public static void Initialize()
        {
            using (var context = new ElmahContext())
            {
                context.Database.Initialize(true);
            }
        }
    }
}
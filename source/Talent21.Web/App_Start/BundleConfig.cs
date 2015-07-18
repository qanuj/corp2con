using System.Web;
using System.Web.Optimization;

namespace Talent21.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/jquery.flexmenu.css",
                      "~/Content/css/owl.carousel.css",
                      "~/Content/css/animate.css",
                      "~/Content/css/jquery.fancybox.css",
                      "~/Content/css/jquery.nouislider.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/style.css",
                      "~/Content/css/site.css"));

            bundles.Add(new StyleBundle("~/Content/spa").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/jquery.flexmenu.css",
                      "~/Content/css/owl.carousel.css",
                      "~/Content/css/animate.css",
                      "~/Content/css/jquery.fancybox.css",
                      "~/Content/css/jquery.nouislider.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/style.css",
                      "~/Content/css/spa.css"));


            bundles.Add(new ScriptBundle("~/script/site")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/site/modernizr.custom.79639.js")
                .Include("~/Scripts/site/bootstrap.min.js")
                .Include("~/Scripts/site/retina.min.js")
                .Include("~/Scripts/site/scrollReveal.min.js")
                .Include("~/Scripts/site/jquery.flexmenu.js")
                .Include("~/Scripts/site/jquery.ba-cond.min.js")
                .Include("~/Scripts/site/jquery.slitslider.js")
                .Include("~/Scripts/site/owl.carousel.min.js")
                .Include("~/Scripts/site/parallax.js")
                .Include("~/Scripts/site/jquery.counterup.min.js")
                .Include("~/Scripts/site/waypoints.min.js")
                .Include("~/Scripts/site/jquery.nouislider.all.min.js")
                .Include("~/Scripts/site/bootstrap-wysiwyg.js")
                .Include("~/Scripts/site/jquery.hotkeys.js")
                .Include("~/Scripts/site/jflickrfeed.min.js")
                .Include("~/Scripts/site/fancybox.pack.js")
                .Include("~/Scripts/site/magic.js")
                .Include("~/Scripts/site/settings.js")
            );

            bundles.Add(new ScriptBundle("~/script/spa")
                .Include("~/Scripts/site/modernizr.custom.79639.js")
                .Include("~/Scripts/site/bootstrap.min.js")
                .Include("~/Scripts/site/retina.min.js")
                .Include("~/Scripts/site/scrollReveal.min.js")
                .Include("~/Scripts/site/jquery.flexmenu.js")
                .Include("~/Scripts/site/jquery.ba-cond.min.js")
                .Include("~/Scripts/site/jquery.slitslider.js")
                .Include("~/Scripts/site/owl.carousel.min.js")
                .Include("~/Scripts/site/parallax.js")
                .Include("~/Scripts/site/jquery.counterup.min.js")
                .Include("~/Scripts/site/waypoints.min.js")
                .Include("~/Scripts/site/jquery.nouislider.all.min.js")
                .Include("~/Scripts/site/bootstrap-wysiwyg.js")
                .Include("~/Scripts/site/jquery.hotkeys.js")
                .Include("~/Scripts/site/jflickrfeed.min.js")
                .Include("~/Scripts/site/fancybox.pack.js")
                .Include("~/Scripts/site/magic.js")
                .Include("~/Scripts/site/settings.js")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/angular.js")
                .Include("~/Scripts/angular-*")
                .Include("~/app/app.js")
                .Include("~/Scripts/modules/*.js")
                .Include("~/app/app.*")
                .Include("~/app/router/*.js")
                .Include("~/app/directives/*.js")
                .Include("~/app/services/*.js")
                .IncludeDirectory("~/app/views/", "*.js", true)
            );

        }
    }
}

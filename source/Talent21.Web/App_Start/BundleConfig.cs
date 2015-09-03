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

            bundles.Add(new StyleBundle("~/Content/site/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/owl.carousel.css",
                      "~/Content/css/animate.css",
                      "~/Content/css/jquery.fancybox.css",
                      "~/Content/css/jquery.nouislider.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/style.css",
                      "~/Content/css/site.css"));

            bundles.Add(new StyleBundle("~/Content/spa/css-1").Include(
                "~/assets/global/plugins/font-awesome/css/font-awesome.css",
                "~/assets/global/plugins/simple-line-icons/simple-line-icons.css",
                "~/assets/global/plugins/bootstrap/css/bootstrap.css",
                "~/assets/global/plugins/uniform/css/uniform.default.css",
                "~/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.css",
                "~/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css"));

            bundles.Add(new StyleBundle("~/Content/spa/css-2").Include(
                "~/assets/global/css/components.css",
                "~/assets/global/css/plugins.css",
                "~/assets/admin/layout/css/layout.css",
                "~/assets/admin/pages/css/*.css",
                "~/assets/admin/layout/css/themes/darkblue.css",
                "~/assets/admin/layout/css/custom.css"));


            bundles.Add(new ScriptBundle("~/script/site")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/site/modernizr.custom.79639.js")
                .Include("~/Scripts/site/bootstrap.min.js")
                .Include("~/Scripts/site/retina.min.js")
                .Include("~/Scripts/site/scrollReveal.min.js")
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

            bundles.Add(new ScriptBundle("~/script/ie9")
                .Include("~/assets/global/plugins/respond.js")
                .Include("~/assets/global/plugins/excanvas.js")
             );

            bundles.Add(new ScriptBundle("~/script/vendors")
                .Include("~/assets/global/plugins/jquery.min.js")
                .Include("~/assets/global/plugins/jquery.migrate.min.js")
                .Include("~/assets/global/plugins/bootstrap/js/bootstrap.min.js")
                .Include("~/assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js")
                .Include("~/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js")
                .Include("~/assets/global/plugins/jquery.blockui.min.js")
                .Include("~/assets/global/plugins/jquery.cokie.min.js")
                .Include("~/assets/global/plugins/uniform/jquery.uniform.min.js")
                .Include("~/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js")
                .Include("~/assets/global/plugins/angularjs/angular.min.js")
                .Include("~/assets/global/plugins/angularjs/angular-sanitize.min.js")
                .Include("~/assets/global/plugins/angularjs/angular-touch.min.js")
                .Include("~/assets/global/plugins/angularjs/plugins/angular-ui-router.min.js")
                .Include("~/assets/global/plugins/angularjs/plugins/ocLazyLoad.min.js")
                .Include("~/assets/global/plugins/angularjs/plugins/ui-bootstrap-tpls.min.js")
                .Include("~/assets/global/scripts/metronic.js")
                .Include("~/Scripts/vendors/*.js")
                .Include("~/Scripts/angular-*")
                .Include("~/Scripts/rzslider.js")
             );

            bundles.Add(new ScriptBundle("~/script/spa")
                .Include("~/app/app.js")
                .Include("~/Scripts/modules/*.js")
                .Include("~/app/app.*")
                .Include("~/app/router/*.js")
                .Include("~/app/controllers/*.js")
                .Include("~/app/directives/*.js")
                .Include("~/app/services/*.js")
                .IncludeDirectory("~/app/views/", "*.js", true)
            );

        }
    }
}

using System.Web;
using System.Web.Optimization;

namespace VivoosVR.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(

                        /*"~/Scripts/modernizr-*"*/));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Bundles/css/Theme").Include(
            //    //"~/bower_components/Waves/dist/waves.css",

            //    //"~/Content/Theme/app.less",
            //    ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/bower_components/animate.css/animate.css",

                "~/bower_components/ng-notify/dist/ng-notify.min.css",
                "~/bower_components/angular-ui-grid/ui-grid.css", // angular-ui-grid
                "~/Content/Theme/ui-grid-theme.css",
                "~/bower_components/jquery.bootgrid/dist/jquery.bootgrid.css", // bootgrid
                "~/bower_components/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.css",
                "~/VivoosVR.Web/bower_components/sweetalert/dist/sweetalert.css",
                "~/bower_components/material-design-iconic-font/dist/css/material-design-iconic-font.css",
                "~/bower_components/eonasdan-bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.css", // datetime picker

                "~/Content/Common.css",
                "~/Content/Theme/app_1.css",
                "~/Content/Theme/app_2.css",
                "~/Content/site.css"

                ));

            bundles.Add(new StyleBundle("~/Content/js").Include(
                "~/bower_components/less/dist/less.js",
                "~/bower_components/moment/moment.js",
                "~/bower_components/bootstrap-growl/bootstrap-notify.js",
                "~/bower_components/Waves/dist/waves.js",
                "~/bower_components/bootstrap-growl/jquery.bootstrap-growl.js",
                "~/bower_components/sweetalert/dist/sweetalert-dev.js",
                "~/bower_components/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.js",
                "~/Content/Theme/site.js",
                "~/bower_components/angular/angular.js",
                "~/bower_components/angular-route/angular-route.js",
                "~/bower_components/eonasdan-bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js", // datetime picker
                "~/bower_components/jquery.bootgrid/dist/jquery.bootgrid.js", // bootgrid
                "~/bower_components/jquery.bootgrid/dist/jquery.bootgrid.fa.js", // bootgrid
                "~/bower_components/angular-ui-grid/ui-grid.js", // angular-ui-grid
                "~/bower_components/ng-notify/dist/ng-notify.min.js"
                ));




            bundles.Add(new ScriptBundle("~/Bundles/angular").IncludeDirectory("~/Angular", "*.js", true));

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif

        }
    }
}

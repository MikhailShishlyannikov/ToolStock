using System.Web;
using System.Web.Optimization;

namespace Sam.ToolStock.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            bundles.UseCdn = true;

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/Template/css")
                .Include(
                    "~/Content/normalize.css",
                    "~/Content/bootstrap.css",
                    //"~/Content/font-awesome.css",
                    //"~/Content/themify-icons.css",
                    //"~/Content/pe-icon-7-stroke.css",
                    "~/Content/flag-icon.css",
                    "~/Content/cs-skin-elastic.css",
                    "~/Content/dataTables.bootstrap.min.css",
                    "~/Content/style.css",
                    "~/Content/chartist.css",
                    "~/Content/jqvmap.css",
                    "~/Content/weather-icons.css",
                    "~/Content/fullcalendar.css",
                    "~/Content/style.css"
            ));

            bundles.Add(new ScriptBundle("~/Content/Template/js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/modernizr-{version}.js",
                "~/Scripts/bootstrap.bundle.js",
                "~/Scripts/jquery.matchHeight.js",
                //"~/Scripts/inputmask/inputmask.js",
                //"~/Scripts/inputmask/jquery.inputmask.js",
                "~/Scripts/main.js",
                "~/Scripts/data-table/datatables.min.js",
                "~/Scripts/data-table/dataTables.bootstrap.min.js",
                "~/Scripts/data-table/dataTables.buttons.min.js",
                "~/Scripts/data-table/buttons.bootstrap.min.js",
                "~/Scripts/data-table/jszip.min.js",
                "~/Scripts/data-table/vfs_fonts.js",
                "~/Scripts/data-table/buttons.html5.min.js",
                "~/Scripts/data-table/buttons.print.min.js",
                "~/Scripts/data-table/buttons.colVis.min.js",
                "~/Scripts/datatables-init.js",
                "~/Scripts/Chart.bundle.js",
                "~/Scripts/chartist.js",
                "~/Scripts/chartist-plugin-legend.js",
                "~/Scripts/jquery.flot.js",
                "~/Scripts/jquery.flot.pie.min.js",
                "~/Scripts/jquery.flot.spline.js",
                "~/Scripts/jquery.simpleWeather.js",
                "~/Scripts/weather-init.js",
                "~/Scripts/moment.js",
                "~/Scripts/fullcalendar.js",
                "~/Scripts/fullcalendar-init.js",
                "~/Scripts/CustomScript.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/modernizr-{version}.js",
                "~/Scripts/bootstrap.bundle.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/inputmask").Include(
                "~/Scripts/inputmask/inputmask.js",
                "~/Scripts/inputmask/jquery.inputmask.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                "~/Scripts/CustomScript.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
        }
    }
}

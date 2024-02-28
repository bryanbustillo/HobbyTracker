using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace HobbyTrackerCliente
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new Bundle("~/bundles/complementos").Include(
                        /*"~/Scripts/scripts.js",*/ //js de la plantilla
                        "~/Scripts/fontawesome/all.min.js", //para los íconos de la plantilla
                        "~/Scripts/DataTables/jquery.DataTables.js",
                        "~/Scripts/DataTables/DataTables.responsive.js",
                        "~/Scripts/loadingoverlay/loadingoverlay.min.js", //js para pantallas de carga
                        "~/Scripts/sweetalert.min.js",//libreria para los alerts
                        "~/Scripts/jquery-ui-1.13.2.js")); //script para el datepicker jquery



            //bundles.Add(new Bundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css",
                      "~/Content/DataTables/css/jquery.dataTables.css",
                      "~/Content/DataTables/css/responsive.dataTables.css",
                      "~/Content/sweetalert.css")); //css de los alerts
        }
    }
}

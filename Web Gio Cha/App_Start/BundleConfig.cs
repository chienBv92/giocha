using System.Web;
using System.Web.Optimization;

namespace Web_Gio_Cha
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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/input").Include(
                       "~/Scripts/jquery.numeric.js",
                       "~/Scripts/WebDuhoc.numeric.js",
                       "~/Scripts/WebDuhoc.Utility.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                       "~/Content/bootstrap.css",
                        "~/Content/cm-form.css",
                        "~/Content/cm-layout.css",
                       "~/Content/jquery-ui.css",
                       "~/Content/tokeiShip.css",
                       "~/Content/ShipdataTables.css",
                       "~/Content/Responsive.css",
                       "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                       "~/Scripts/jquery.dataTables.js",
                       "~/Scripts/dataTables.fixedColumns.js",
                       "~/Scripts/jquery.cookie.js",
                       "~/Scripts/fnStandingRedraw.js",
                       "~/Scripts/WebDuhoc.dataTables.js"));

            bundles.Add(new StyleBundle("~/Content/common").Include(
              "~/Content/jquery.dataTables.css",
              "~/Content/datepicker.css"
              ));

            bundles.Add(new ScriptBundle("~/bundles/jquery/bootstrapPlugins").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/plugins/datepicker/bootstrap-datepicker.js",
                      "~/Scripts/plugins/datepicker/locales/bootstrap-datepicker.ja.js",
                      "~/Scripts/plugins/bootstrap-dialog.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery/plugins/dragOn").Include(
             "~/Scripts/plugins/dragOn.js"
             , "~/Scripts/plugins/barOn.js"));
        }
    }
}

using System.Web;
using System.Web.Optimization;

namespace MakeIt.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/metisMenu").Include(
                      "~/Scripts/plugins/metisMenu/metisMenu.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/sb-admin-2").Include(
                     "~/Scripts/sb-admin-2.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/sb-admin-2.css",
                      "~/Content/css/plugins/metisMenu/metisMenu.min.css",
                      "~/font-awesome-4.1.0/css/font-awesome.min.css"));
        }
    }
}

using System.Web;
using System.Web.Optimization;

namespace Kickoff4Kids420
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //bundles.Add(new ScriptBundle("~/bundles/template").Include(
            //            "~/Scripts/back-to-top.js"
            //            ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/style.css",
                      "~/Content/header1.css",
                      "~/Content/responsive.css",                      
                      "~/Content/font-awesome/css/font-awesome.css",
                      "~/Content/flexslider.css",
                      "~/Content/parallax-slider.css",
                      "~/Content/red.css",
                      "~/Content/header1-red.css",
                      "~/Content/page_log_reg_v1.css"
                      ));
            bundles.Add(new StyleBundle("~/pricing/css").Include(
                        "~/Content/page_pricing.css"
                ));
            bundles.Add(new StyleBundle("~/404/css").Include(
                        "~/Content/page_404_error.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                    "~/Scripts/Custom/k4kcart.js"
                    
                ));
            bundles.Add(new ScriptBundle("~/bundles/index").Include(                   
                    "~/Scripts/jquery.flexslider-min.js",
                    "~/Scripts/jquery.cslider.js",
                    "~/Scripts/app.js",
                    "~/Scripts/index.js",
                    "~/Scripts/indexInit.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/reports").Include(
                    "~/Scripts/Custom/visualize.jQuery.js",
                    "~/Scripts/Custom/highcharts.js"
                ));


            
        }
    }
}
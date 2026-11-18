using System.Web.Optimization;

namespace App.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new Bundle("~/bundles/scripts")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/bootstrap/bootstrap.js")
                .Include("~/Scripts/bootstrap/respond.js")
                .Include("~/Scripts/angular/angular.js")
                .Include("~/Scripts/angular/angular-route.js")
                .Include("~/Scripts/angular/angular-cookies.js")
                .Include("~/Scripts/angular/angular-filter.js")
                .Include("~/Scripts/angular-ui/ui-bootstrap-tpls.js")
                .Include("~/Scripts/fancybox/jquery.fancybox.js")
                .Include("~/Scripts/fancybox/jquery.fancybox-thumbs.js")
                .Include("~/Scripts/datepicker/jquery.datetimepicker.full.js")
                .Include("~/Scripts/newdatepicker/bootstrap-datepicker.js")
                .Include("~/Scripts/scheduler/jqx-all.js")
                );
                        
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
                     
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/fancybox/*.css",
                      "~/Content/datepicker/jquery.datetimepicker.css",
                      "~/Content/newdatepicker/bootstrap-datepicker.css",
                      "~/Content/newdatepicker/doc.css",
                      "~/Content/scheduler/*.css"));
        }
    }
}

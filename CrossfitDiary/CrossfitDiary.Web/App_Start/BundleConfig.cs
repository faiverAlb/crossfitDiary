using System.Web;
using System.Web.Optimization;

namespace CrossfitDiary.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                   .Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/bootstrap-select/bootstrap-select.js",
                "~/Scripts/knockout-3.4.0.js",
                "~/Scripts/ko.bindingHandlers.js"
                ));


            /*   Crossfitter   */
            bundles.Add(new ScriptBundle("~/bundles/crossfitter")
                .Include(
                "~/Scripts/Crossfitter/CrossfitterController.js"
                , "~/Scripts/Crossfitter/SimpleRoutine.js"
                , "~/Scripts/Crossfitter/ExerciseMeasureType.js"
                , "~/Scripts/Crossfitter/WorkoutType.js"
                , "~/Scripts/Crossfitter/ExerciseMeasureTypeValue.js"
                , "~/Scripts/Crossfitter/CrossfitterService.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/css/font-awesome.min.css"));

        }
    }
}
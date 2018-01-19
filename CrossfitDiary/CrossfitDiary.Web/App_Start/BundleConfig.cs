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

            
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/bootstrap-select/bootstrap-select.js"));


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-3.4.0.js",
                "~/Scripts/knockout.validation.js",
                "~/Scripts/ko.bindingHandlers.js",
                "~/Scripts/typings/q/q.min.js"
            ));


            /*   --Crossfitter--   */
            
            /* Manage workout */
            bundles.Add(new ScriptBundle("~/bundles/crossfitter/manageWorkout")
                .Include(
                    "~/Scripts/jqueryPlugins/jquery.inputmask.bundle.min.js"
                    , "~/Scripts/bootstrap-datepicker/moment.min.js"
                    , "~/Scripts/bootstrap-datepicker/moment-with-locales.min.js"
                    , "~/Scripts/bootstrap-datepicker/bootstrap-datetimepicker.min.js"
                    , "~/Scripts/Crossfitter/CrossfitterController.js"
                    , "~/Scripts/Crossfitter/SimpleRoutine.js"
                    , "~/Scripts/Crossfitter/ExerciseMeasureType.js"
                    , "~/Scripts/Crossfitter/WorkoutType.js"
                    , "~/Scripts/Crossfitter/ExerciseMeasureTypeValue.js"
                    , "~/Scripts/BaseTypeScript/ServerProcessedViewModel.js"
                    , "~/Scripts/BaseTypeScript/BaseService.js"
                    , "~/Scripts/BaseTypeScript/CrossfitterService.js"

                    , "~/Scripts/Crossfitter/ManageWorkout/CreateWorkoutController.js"
                    , "~/Scripts/Crossfitter/ManageWorkout/ChooseExistingWorkoutController.js"
                    , "~/Scripts/Crossfitter/ManageWorkout/LogWorkoutController.js"
                    , "~/Scripts/Crossfitter/ManageWorkout/ManageWorkoutController.js"
                ));


            /* Statistics */
            bundles.Add(new ScriptBundle("~/bundles/crossfitter/statistics")
                .Include(
                    "~/Scripts/BaseTypeScript/ServerProcessedViewModel.js"
                    , "~/Scripts/BaseTypeScript/BaseService.js"
                    , "~/Scripts/BaseTypeScript/CrossfitterService.js"
                    , "~/Scripts/BaseTypeScript/BasicParameters.js"
                    , "~/Scripts/BaseTypeScript/FilterableViewModel.js"
                    , "~/Scripts/Crossfitter/Pride/PrideController.js"
                ));

            /*   --Home--   */
            bundles.Add(new ScriptBundle("~/bundles/home")
                .Include(
                     "~/Scripts/BaseTypeScript/ServerProcessedViewModel.js"
                    , "~/Scripts/BaseTypeScript/BaseService.js"
                    , "~/Scripts/BaseTypeScript/CrossfitterService.js"
                    ,"~/Scripts/Home/HomePageController.js"));


            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.min.css"
                    , "~/Content/css/font-awesome.min.css"
                    , "~/Content/bootstrap-datepicker/bootstrap-datepicker.css"));

        }
    }
}
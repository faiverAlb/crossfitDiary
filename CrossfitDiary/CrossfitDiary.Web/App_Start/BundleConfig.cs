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

            bundles.Add(new ScriptBundle("~/bundles/font-awesome").Include(
                        "~/Scripts/font-awesome/fontawesome-all.js"));

            
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.bundle.js"/*,
                "~/Scripts/bootstrap-select/bootstrap-select.js"*/));


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
                    , "~/Scripts/gijgo/combined/gijgo.min.js"

                    , "~/Scripts/Pages/General/BaseController.js"
                    , "~/Scripts/Pages/General/BaseService.js"
                    , "~/Scripts/Pages/General/BaseKeyValuePairModel.js"
                    , "~/Scripts/Pages/General/BasicParameters.js"
                    , "~/Scripts/Pages/General/CrossfitterService.js"
                    , "~/Scripts/Pages/General/ServerProcessedViewModel.js"
                    , "~/Scripts/Pages/General/ErrorMessageViewModel.js"

                    , "~/Scripts/Pages/Models/ExerciseMeasureTypeViewModelObservable.js"
                    , "~/Scripts/Pages/Models/ExerciseMeasureViewModelObservable.js"
                    , "~/Scripts/Pages/Models/ExerciseViewModelObservable.js"
                    , "~/Scripts/Pages/Models/WorkoutViewModelObservable.js"
                    , "~/Scripts/Pages/Models/ToLogWorkoutViewModelObservable.js"


                    , "~/Scripts/Pages/Crossfitter/ManageWorkout/LogWorkoutController.js"
                    , "~/Scripts/Pages/Crossfitter/ManageWorkout/ManageWorkoutController.js"
                ));


            /* Statistics */
            bundles.Add(new ScriptBundle("~/bundles/crossfitter/statistics")
                .Include(
                    "~/Scripts/Pages/General/ServerProcessedViewModel.js"
                    , "~/Scripts/Pages/General/BaseService.js"
                    , "~/Scripts/Pages/General/CrossfitterService.js"
                    , "~/Scripts/Pages/General/BasicParameters.js"
                    , "~/Scripts/Pages/General/FilterableViewModel.js"
                    , "~/Scripts/Pages/General/ErrorMessageViewModel.js"

                    , "~/Scripts/Pages/Models/ObservablePersonExerciseRecord.js"
                    , "~/Scripts/Pages/Crossfitter/Pride/PrideController.js"
                ));

            /*   --Home--   */
            bundles.Add(new ScriptBundle("~/bundles/home")
                .Include(
                    "~/Scripts/Pages/General/ServerProcessedViewModel.js"
                    , "~/Scripts/Pages/General/BaseService.js"
                    , "~/Scripts/Pages/General/CrossfitterService.js"
                    , "~/Scripts/Pages/General/BaseController.js"
                    , "~/Scripts/Pages/General/ErrorMessageViewModel.js"
                    , "~/Scripts/Pages/Home/TemplateNames.js"
                    , "~/Scripts/Pages/Home/HomePageController.js"));

            bundles.Add(new ScriptBundle("~/bundles/general-models")
                .Include(
                    "~/Scripts/Pages/Models/ViewModels/ExerciseMeasureTypeValue.js"
                    , "~/Scripts/Pages/Models/ViewModels/WorkoutType.js"
                    , "~/Scripts/Pages/Models/ViewModels/ExerciseMeasureType.js"
                    , "~/Scripts/Pages/Models/ViewModels/SimpleRoutine.js"
                    , "~/Scripts/Pages/Models/ViewModels/ExerciseMeasureTypeViewModel.js"
                    , "~/Scripts/Pages/Models/ViewModels/ExerciseMeasureViewModel.js"
                    , "~/Scripts/Pages/Models/ViewModels/ExerciseViewModel.js"
                    , "~/Scripts/Pages/Models/ViewModels/WorkoutViewModel.js"
                    , "~/Scripts/Pages/Models/ViewModels/ToLogWorkoutViewModel.js"
                    , "~/Scripts/Pages/Models/ViewModels/PersonExerciseRecord.js"
                ));

            bundles.Add(new StyleBundle("~/Content/login")
                .Include("~/Content/pages/login-page.css"));

            bundles.Add(new StyleBundle("~/Content/home")
                .Include("~/Content/pages/home.css"));

            bundles.Add(new StyleBundle("~/Content/pride")
                .Include("~/Content/pages/pride.css"));

            bundles.Add(new StyleBundle("~/Content/manage-workout")
                .Include("~/Content/pages/manage-workout.css")
                .Include("~/Content/gijgo/combined/gijgo_style.css"));

        }
    }
}
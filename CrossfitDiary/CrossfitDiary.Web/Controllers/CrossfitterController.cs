using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CrossfitDiary.Service.Interfaces;
using CrossfitDiary.Web.MvcHelpers;
using CrossfitDiary.Web.ViewModels;

namespace CrossfitDiary.Web.Controllers
{
    public partial class CrossfitterController : Controller
    {
        private readonly IExerciseService _exerciseService;
        private readonly IWorkoutService _workoutService;

        public CrossfitterController(IExerciseService exerciseService, IWorkoutService workoutService)
        {
            _exerciseService = exerciseService;
            _workoutService = workoutService;
        }

        public virtual ActionResult CreateNewWorkout()
        {
            IEnumerable<ExerciseViewModel> viewModels = Mapper.Map<IEnumerable<ExerciseViewModel>>(_exerciseService.GetExercises());
            var model = new
            {
                exercises = viewModels,
                workoutTypes = EnumHelper.ToList(typeof(WorkoutTypeViewModel)).OrderBy(x => x.Key)
            };
            return View(model);
        }

        public virtual ActionResult LogWorkout()
        {
            var logWorkoutViewModel = new LogWorkoutViewModel
            {
                AvailableWorkouts = Mapper.Map<IEnumerable<WorkoutViewModel>>(_workoutService.GetAvailableWorkouts())
            };
            return View(logWorkoutViewModel);
        }

        public virtual ActionResult ManageWorkout()
        {
            IEnumerable<ExerciseViewModel> viewModels = Mapper.Map<IEnumerable<ExerciseViewModel>>(_exerciseService.GetExercises());
            var model = new
            {
                exercises = viewModels,
                workoutTypes = EnumHelper.ToList(typeof(WorkoutTypeViewModel)).OrderBy(x => x.Key)
            };
            return View(model);
        }
        

    }
}

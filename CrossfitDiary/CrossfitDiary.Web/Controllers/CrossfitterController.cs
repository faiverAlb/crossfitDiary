using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CrossfitDiary.Service;
using CrossfitDiary.Service.Interfaces;
using CrossfitDiary.Web.MvcHelpers;
using CrossfitDiary.Web.ViewModels;
using Microsoft.AspNet.Identity;

namespace CrossfitDiary.Web.Controllers
{
    public partial class CrossfitterController : Controller
    {
        private readonly CrossfitterService _crossfitterService;
        private readonly IExerciseService _exerciseService;
        private readonly IWorkoutService _workoutService;

        public CrossfitterController(CrossfitterService crossfitterService, IExerciseService exerciseService, IWorkoutService workoutService)
        {
            _crossfitterService = crossfitterService;
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

//        public virtual ActionResult LogWorkout()
//        {
//            var logWorkoutViewModel = new LogWorkoutViewModel
//            {
//                AvailableWorkouts = Mapper.Map<IEnumerable<WorkoutViewModel>>(_workoutService.GetAvailableWorkouts())
//            };
//            return View(logWorkoutViewModel);
//        }

        public virtual ActionResult ManageWorkout(DateTime? date, int? crossfitterWorkoutId)
        {
            var model = new
            {
                workoutTypes = EnumHelper.ToList(typeof(WorkoutTypeViewModel)).OrderBy(x => x.Key),
                planDate = date?.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz") ?? DateTime.Now.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz"),
                crossfitterWorkout = crossfitterWorkoutId.HasValue? Mapper.Map<ToLogWorkoutViewModel>(_crossfitterService.GetCrossfitterWorkout(HttpContext.User.Identity.GetUserId(), crossfitterWorkoutId.Value)) : null
            };
             
            return View(model);
        }

        public virtual ActionResult Pride()
        {
            return View();
        }
        

    }
}

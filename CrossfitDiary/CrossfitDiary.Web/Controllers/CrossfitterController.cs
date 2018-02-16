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

        public virtual ActionResult ManageWorkout()
        {
            return View();
        }

        public virtual ActionResult Pride()
        {
            return View();
        }
        

    }
}

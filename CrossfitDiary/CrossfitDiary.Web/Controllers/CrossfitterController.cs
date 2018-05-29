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
        private readonly IWorkoutService _workoutService;
        public CrossfitterController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        public virtual ActionResult ManageWorkout(int? workoutId, int? crossfitterWorkoutId)
        {
            ViewBag.Title = "Manage Workout";

            ViewBag.PredefindWorkoutId = workoutId;
            if (workoutId.HasValue)
            {
                ViewBag.PredefindWorkout = Mapper.Map<WorkoutViewModel>(_workoutService.GetWorkout(workoutId.Value));
            }

            //TODO: Check persmissions!
            ViewBag.CrossfitterWorkoutId = crossfitterWorkoutId;

            return View();
        }

        public virtual ActionResult Pride()
        {
            ViewBag.Title = "What was done";
            return View();
        }
        

    }
}

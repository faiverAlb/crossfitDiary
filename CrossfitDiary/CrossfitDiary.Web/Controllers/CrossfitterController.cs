using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using CrossfitDiary.Service;
using CrossfitDiary.Service.Interfaces;
using CrossfitDiary.Web.MvcHelpers;
using CrossfitDiary.Web.ViewModels;

namespace CrossfitDiary.Web.Controllers
{
    public partial class CrossfitterController : Controller
    {
        private readonly IExerciseService _exerciseService;

        public CrossfitterController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        public virtual ActionResult Index()
        {
            IEnumerable<ExerciseViewModel> viewModels = Mapper.Map<IEnumerable<ExerciseViewModel>>(_exerciseService.GetExercises());
            var model = new
            {
                exercises = viewModels,
                workoutTypes = EnumHelper.ToList(typeof (WorkoutTypeViewModel))
            };
            return View(model);
        }

    }
}

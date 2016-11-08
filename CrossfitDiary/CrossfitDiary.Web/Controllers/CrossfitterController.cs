using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using CrossfitDiary.Service;
using CrossfitDiary.Service.Interfaces;
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
            var exercises = _exerciseService.GetExercises();
            IEnumerable<ExerciseViewModel> viewModels = Mapper.Map<IEnumerable<ExerciseViewModel>>(exercises);
            return View(viewModels);
        }

    }
}

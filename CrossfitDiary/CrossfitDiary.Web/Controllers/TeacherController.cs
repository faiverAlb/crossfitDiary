using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using CrossfitDiary.Model;
using CrossfitDiary.Service;
using CrossfitDiary.Web.ViewModels;

namespace CrossfitDiary.Web.Controllers
{
    public partial class TeacherController : Controller
    {
        private readonly IExerciseService _exerciseService;

        public TeacherController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        public virtual ActionResult Index()
        {

            var exercises = _exerciseService.GetExercises();
            var viewModels = Mapper.Map<IEnumerable<Exercise>, IEnumerable<ExerciseViewModel>>(exercises);
            return View(viewModels);
        }

    }
}

using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CrossfitDiary.Service;
using CrossfitDiary.Web.ViewModels;
using CrossfitDiary.Web.ViewModels.Pride;
using Microsoft.AspNet.Identity;

namespace CrossfitDiary.Web.Controllers
{
    public partial class PersonsController : Controller
    {
        private readonly CrossfitterService _crossfitterService;

        public PersonsController(CrossfitterService crossfitterService)
        {
            _crossfitterService = crossfitterService;
        }
        public virtual ActionResult Index(string userId = null, int? exerciseId = null)
        {
            string userIdToCheck = string.IsNullOrEmpty(userId)? User.Identity.GetUserId(): userId;
            PersonDataViewModel personDataViewModel = new PersonDataViewModel()
            {
                AllWorkouts = _crossfitterService.GetAllCrossfittersWorkouts(userId).Select(Mapper.Map<ToLogWorkoutViewModel>).ToList(),
                PersonMaximums = _crossfitterService.GetPersonMaximumForMainExercises(userIdToCheck).Select(x => Mapper.Map<PersonExerciseMaximumViewModel>(x)).OrderBy(x => x.ExerciseDisplayName).ToList(),
            };
            ViewBag.Title = "Person Page";
            ViewBag.UserId = userId;
            return View(personDataViewModel);
        }
    }
}
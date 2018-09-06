using System.Linq;
using AutoMapper;
using CrossfitDiaryCore.BL.Services;
using CrossfitDiaryCore.Web.ViewModels;
using CrossfitDiaryCore.Web.ViewModels.Pride;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrossfitDiaryCore.Web.Controllers
{
    [Authorize]
    public class PersonsController : Controller
    {
        private readonly ReadWorkoutsService _readWorkoutsService;
        public PersonsController(ReadWorkoutsService readWorkoutsService)
        {
            _readWorkoutsService = readWorkoutsService;
        }


        public IActionResult Index(string userId = null, int? exerciseId = null)
        {
            //            string userIdForMaximums = string.IsNullOrEmpty(userId) ? User.Identity.GetUserId() : userId;
            //            string userIdForWorkouts = exerciseId.HasValue && string.IsNullOrEmpty(userId) ? User.Identity.GetUserId() : userId;
//            string userIdForWorkouts = null;

            PersonDataViewModel personDataViewModel = new PersonDataViewModel()
            {
//                InitialWorkouts = _readWorkoutsService.GetAllCrossfittersWorkouts(userIdForWorkouts, exerciseId, page: 1, pageSize: 30).Select(Mapper.Map<ToLogWorkoutViewModel>).ToList(),
//                PersonMaximums = _readWorkoutsService.GetPersonMaximumForMainExercises(userIdForMaximums, exerciseId).Select(x => Mapper.Map<PersonExerciseMaximumViewModel>(x)).OrderBy(x => x.ExerciseDisplayName).ToList(),
            };
            ViewBag.Title = "Person Page";
            ViewBag.UserId = userId;
            ViewBag.ExerciseId = exerciseId;
            return View(personDataViewModel);
        }

    }
}
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
            ViewBag.Title = "Person Page";
            ViewBag.UserId = userId;
            ViewBag.ExerciseId = exerciseId;
            return View();
        }

    }
}
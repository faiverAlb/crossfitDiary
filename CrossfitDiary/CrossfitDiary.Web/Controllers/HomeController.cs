using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CrossfitDiary.Service;
using CrossfitDiary.Web.Configuration;
using CrossfitDiary.Web.MvcHelpers;
using CrossfitDiary.Web.ViewModels;
using Microsoft.AspNet.Identity;

namespace CrossfitDiary.Web.Controllers
{
//    [RequireHttps]
    public partial class HomeController : Controller
    {
        private readonly CrossfitterService _crossfitterService;
        private readonly ApplicationUserManager _applicationUserManager;

        public HomeController(CrossfitterService crossfitterService, ApplicationUserManager applicationUserManager)
        {
            _crossfitterService = crossfitterService;
            _applicationUserManager = applicationUserManager;
        }
        public virtual ActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                AllWorkouts = _crossfitterService.GetAllCrossfittersWorkouts().Select(x => Mapper.Map<ToLogWorkoutViewModel>(x)).OrderByDescending(x => x.Date).ToList(),
            };

            return View(model: homeViewModel);
        }


        [Authorize]
        public virtual ActionResult Secure()
        {
            ViewBag.Message = "Secure page.";
            return View();
        }

    }
}

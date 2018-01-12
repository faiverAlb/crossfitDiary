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


            var fromDate = DateTime.Now.AddDays(-10);
            var toDate = DateTime.Now.AddDays(10);
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                AllWorkouts = _crossfitterService.GetAllCrossfittersWorkouts(fromDate, toDate).Select(x => Mapper.Map<ToLogWorkoutViewModel>(x)).ToList(),
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

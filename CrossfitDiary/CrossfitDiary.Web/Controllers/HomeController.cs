using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CrossfitDiary.Model;
using CrossfitDiary.Service.Interfaces;
using CrossfitDiary.Web.Configuration;
using CrossfitDiary.Web.MvcHelpers;
using CrossfitDiary.Web.ViewModels;
using Microsoft.AspNet.Identity;

namespace CrossfitDiary.Web.Controllers
{
//    [RequireHttps]
    public partial class HomeController : Controller
    {
        private readonly ICrossfitterService _crossfitterService;
        private readonly ApplicationUserManager _applicationUserManager;

        public HomeController(ICrossfitterService crossfitterService, ApplicationUserManager applicationUserManager)
        {
            _crossfitterService = crossfitterService;
            _applicationUserManager = applicationUserManager;
        }
        public virtual ActionResult Index()
        {
            

            HomeViewModel homeViewModel = new HomeViewModel()
            {
                WeekWorkouts = GetWeekWorkouts()
            };

            return View(model: homeViewModel);
        }

        private List<DayWorkoutViewModel> GetWeekWorkouts()
        {
            var startOfWeek = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            List<ToLogWorkoutViewModel> crossfitterWorkoutsViewModels = _crossfitterService.GetCrossfitterWorkouts(System.Web.HttpContext.Current.User.Identity.GetUserId()
                                                                                                                   , startOfWeek
                                                                                                                   , startOfWeek.AddDays(6)).Select(x => Mapper.Map<ToLogWorkoutViewModel>(x)).ToList();
            var groupedModels = crossfitterWorkoutsViewModels.GroupBy(x => x.Date.DayOfWeek);

            var dayWorkouts = new List<DayWorkoutViewModel>
            {
                GetDayWorkoutViewModelByDate(DayOfWeek.Monday, startOfWeek, groupedModels),
                GetDayWorkoutViewModelByDate(DayOfWeek.Tuesday, startOfWeek.AddDays(1), groupedModels),
                GetDayWorkoutViewModelByDate(DayOfWeek.Wednesday, startOfWeek.AddDays(2), groupedModels),
                GetDayWorkoutViewModelByDate(DayOfWeek.Thursday, startOfWeek.AddDays(3), groupedModels),
                GetDayWorkoutViewModelByDate(DayOfWeek.Friday, startOfWeek.AddDays(4), groupedModels),
                GetDayWorkoutViewModelByDate(DayOfWeek.Saturday, startOfWeek.AddDays(5), groupedModels),
                GetDayWorkoutViewModelByDate(DayOfWeek.Sunday, startOfWeek.AddDays(6), groupedModels),
            };
            return dayWorkouts;
        }


        private DayWorkoutViewModel GetDayWorkoutViewModelByDate(DayOfWeek dayOfWeek, DateTime date, IEnumerable<IGrouping<DayOfWeek, ToLogWorkoutViewModel>> groupedModels)
        {
            var dayWorkout = groupedModels.SingleOrDefault(x => x.Key == dayOfWeek);
            var doneWorkoutes = dayWorkout?.Where(x => x.IsPlanned == false).ToList() ?? new List<ToLogWorkoutViewModel>();
            var planendWorkouts = dayWorkout?.Where(x => x.IsPlanned == true).ToList() ?? new List<ToLogWorkoutViewModel>();
            return new DayWorkoutViewModel
            {
                DayOfWeek = dayOfWeek.ToString(),
                Date = date,
                DoneWorkouts = doneWorkoutes,
                PlannedWorkouts = planendWorkouts
            };
        }


        [Authorize]
        public virtual ActionResult Secure()
        {
            ViewBag.Message = "Secure page.";
            return View();
        }

    }
}

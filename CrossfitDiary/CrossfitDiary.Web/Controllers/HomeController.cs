using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CrossfitDiary.Web.ViewModels;

namespace CrossfitDiary.Web.Controllers
{
    [RequireHttps]
    public partial class HomeController : Controller
    {

        public virtual ActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                WeekWorkouts = new List<DayWorkoutViewModel>()
                {
                    new DayWorkoutViewModel()
                    {
                        Date = DateTime.Now,
                        DayOfWeek = DateTime.Now.DayOfWeek.ToString()
                    },
                    new DayWorkoutViewModel()
                    {
                        Date = DateTime.Now.AddDays(1),
                        DayOfWeek = DateTime.Now.DayOfWeek.ToString()
                    },
                    new DayWorkoutViewModel()
                    {
                        Date = DateTime.Now.AddDays(2),
                        DayOfWeek = DateTime.Now.DayOfWeek.ToString()
                    },
                    new DayWorkoutViewModel()
                    {
                        Date = DateTime.Now.AddDays(3),
                        DayOfWeek = DateTime.Now.DayOfWeek.ToString()
                    },
                    new DayWorkoutViewModel()
                    {
                        Date = DateTime.Now.AddDays(4),
                        DayOfWeek = DateTime.Now.DayOfWeek.ToString()
                    },
                    new DayWorkoutViewModel()
                    {
                        Date = DateTime.Now.AddDays(5),
                        DayOfWeek = DateTime.Now.DayOfWeek.ToString()
                    },
                    new DayWorkoutViewModel()
                    {
                        Date = DateTime.Now.AddDays(6),
                        DayOfWeek = DateTime.Now.DayOfWeek.ToString()
                    },
                }
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

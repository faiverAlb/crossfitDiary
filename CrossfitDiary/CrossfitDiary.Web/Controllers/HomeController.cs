using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CrossfitDiary.Service;
using CrossfitDiary.Web.Configuration;
using CrossfitDiary.Web.ViewModels;

namespace CrossfitDiary.Web.Controllers
{
//    [RequireHttps]
    public partial class HomeController : Controller
    {
        private readonly CrossfitterService _crossfitterService;

        public HomeController(CrossfitterService crossfitterService)
        {
            _crossfitterService = crossfitterService;
        }
        public virtual ActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                AllWorkouts = _crossfitterService.GetAllCrossfittersWorkouts().Select(x => Mapper.Map<ToLogWorkoutViewModel>(x)).OrderByDescending(x => x.Date).ToList(),
            };
            ViewBag.Title = "Home Page";
            return View(model: homeViewModel);
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace CrossfitDiaryCore.Web.Controllers
{
    public class PersonsController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Person Page";
            return View();
        }

    }
}
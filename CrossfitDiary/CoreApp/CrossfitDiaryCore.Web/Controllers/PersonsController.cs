using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrossfitDiaryCore.Web.Controllers
{
    [Authorize]
    public class PersonsController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Person Page";
            return View();
        }

    }
}
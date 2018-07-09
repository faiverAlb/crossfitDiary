using Microsoft.AspNetCore.Mvc;

namespace CrossfitDiaryCore.Web.Controllers
{
    public  class ErrorController : Controller
    {
        // GET
        public virtual IActionResult Index()
        {
            return View("Error");
        }
    }
}
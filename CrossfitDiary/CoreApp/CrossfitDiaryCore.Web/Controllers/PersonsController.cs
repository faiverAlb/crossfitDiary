using Microsoft.AspNetCore.Mvc;

namespace CrossfitDiaryCore.Web.Controllers
{
    public class PersonsController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
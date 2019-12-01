using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrossfitDiaryCore.Web.Controllers
{
    [Authorize]
    public class WodsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
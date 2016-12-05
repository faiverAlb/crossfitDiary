using System.Web.Mvc;

namespace CrossfitDiary.Web.Controllers
{
    [RequireHttps]
    public partial class HomeController : Controller
    {

        public virtual ActionResult Index()
        {
            return View();
        }


        [Authorize]
        public virtual ActionResult Secure()
        {
            ViewBag.Message = "Secure page.";
            return View();
        }

    }
}

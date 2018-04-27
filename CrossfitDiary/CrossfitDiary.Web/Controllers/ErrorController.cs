using System.Web.Mvc;

namespace CrossfitDiary.Web.Controllers
{
    public partial class ErrorController : Controller
    {
        // GET
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}
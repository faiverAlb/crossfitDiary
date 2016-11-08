using System.Web.Mvc;

namespace CrossfitDiary.Web.Controllers
{
    public partial class AdminController : Controller
    {
        //
        // GET: /Admin/

        public virtual ActionResult Index()
        {
            return View();
        }


    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrossfitDiaryCore.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController: Controller
    {
        public IActionResult Login()
        {
            return View("Login");
        }
    }
}
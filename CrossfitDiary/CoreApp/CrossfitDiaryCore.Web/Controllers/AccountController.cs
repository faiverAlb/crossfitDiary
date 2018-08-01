using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CrossfitDiaryCore.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CrossfitDiaryCore.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        public async Task<IActionResult> Confirmation(string provider, string returnUrl = null)
        {
            string redirectUrl = Url.Action("ExternalLogin");
            AuthenticationProperties properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }


        public async Task<IActionResult> ExternalLogin(string returnUrl)
        {
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("ErrorLogin");
            }

            Microsoft.AspNetCore.Identity.SignInResult alreadyCreatedUserInfo = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: true);
            if (alreadyCreatedUserInfo.Succeeded)
            {
                return RedirectToAction("Index", "Persons");
            }

            bool isSucceedCreateUser = await CreateUser(info);
            if (isSucceedCreateUser)
            {
                return RedirectToAction("Index", "Persons");
            }
            return RedirectToAction("ErrorLogin");
        }

        private async Task<bool> CreateUser(ExternalLoginInfo info)
        {
            var user = new ApplicationUser
            {
                UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                Email = info.Principal.FindFirstValue(ClaimTypes.Email)
            };


            IdentityResult createUserStatus = await _userManager.CreateAsync(user);
            if (createUserStatus.Succeeded)
            {
                createUserStatus = await _userManager.AddLoginAsync(user, info);
                if (createUserStatus.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: true);
                    return true;
                }
            }
            return false;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ErrorLogin()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


    }
}
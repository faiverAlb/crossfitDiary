using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CrossfitDiary.DAL.EF.DataContexts;
using CrossfitDiary.Model;
using CrossfitDiary.Web.Configuration;
using CrossfitDiary.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace CrossfitDiary.Web.Controllers
{
    [AllowAnonymous]
    public partial class AccountController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _applicationSignInManager;


        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager applicationSignInManager)
        {
            _userManager = userManager;
            _applicationSignInManager = applicationSignInManager;
        }


        [HttpGet]
        public virtual ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

            var model = new LoginModel() {ReturnUrl = returnUrl};
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Login(string provider, string returnUrl)
        {
            return new ChallengeResult(provider, Url.Action(MVC.Account.ActionNames.ExternalLoginCallback, "Account", new { ReturnUrl = returnUrl }));
        }

        public virtual ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        public virtual async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
                return RedirectToAction(MVC.Account.Views.ExternalLoginFailure, new {ReturnUrl = returnUrl});

            
            var result = await _applicationSignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                default:
                    var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                    if (info == null)
                        return View(MVC.Account.Views.ExternalLoginFailure);

                    var user = new ApplicationUser { UserName = loginInfo.Email, Email = loginInfo.Email };
                    var createdUser = await _userManager.CreateAsync(user);
                    if (createdUser.Succeeded)
                    {
                        createdUser = await _userManager.AddLoginAsync(user.Id, info.Login);
                        if (createdUser.Succeeded)
                        {
                            await _applicationSignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                            return RedirectToLocal(returnUrl);
                        }
                    }
                    return View(MVC.Account.Views.ExternalLoginFailure);
            }
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
        }

        [AllowAnonymous]
        public virtual ActionResult ExternalLoginFailure()
        {
            return View();
        }
       
    }

    class ChallengeResult : HttpUnauthorizedResult
    {
        public string LoginProvider { get; set; }
        public string RedirectUri { get; set; }

        public ChallengeResult(string provider, string redirectUri)
        {
            LoginProvider = provider;
            RedirectUri = redirectUri;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var properties = new AuthenticationProperties {RedirectUri = RedirectUri};
            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        }
    }
}
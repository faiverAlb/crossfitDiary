using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CrossfitDiaryCore.BL.Services;
using CrossfitDiaryCore.Web.ViewModels;
using CrossfitDiaryCore.Web.ViewModels.Pride;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrossfitDiaryCore.Web.Controllers
{
    [Authorize]
    public class PersonsController : Controller
    {
        private readonly ReadUsersService _readUsersService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PersonsController(ReadUsersService readUsersService, IHttpContextAccessor httpContextAccessor)
        {
            _readUsersService = readUsersService;
            _httpContextAccessor = httpContextAccessor;
        }


        public IActionResult Index(string userId = null, int? exerciseId = null)
        {
            ViewBag.Title = "Person Page";
            ViewBag.UserId = userId;
            ViewBag.ExerciseId = exerciseId;
            string currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.showOnlyUserWods = _readUsersService.GetShowOnlyUserWods(currentUserId);
            return View();
        }

        /// <summary>
        ///     Change setting about display of user wods
        /// </summary>
        [HttpPost]
        [Route("api/setShowOnlyUserWods")]
        public async Task SetShowOnlyUserWods([FromQuery] bool showOnlyUserWods)
        {
            var test = 123;

        }


    }
}
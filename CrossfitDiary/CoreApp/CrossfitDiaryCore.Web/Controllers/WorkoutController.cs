using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CrossfitDiaryCore.BL.Services;
using CrossfitDiaryCore.Model;
using CrossfitDiaryCore.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CrossfitDiaryCore.Web.Controllers
{
    [Authorize]
    public class WorkoutController : Controller
    {
        #region members
        private readonly ReadWorkoutsService _readWorkoutsService;
        private readonly ManageWorkoutsService _manageWorkoutsService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMemoryCache _memoryCache;
            
        private string _allMainpageResultsConst = "all-mainpage-results";

        #endregion

        #region constructors

        public WorkoutController(ReadWorkoutsService readWorkoutsService
            , ManageWorkoutsService manageWorkoutsService
            , IMapper mapper
            , IHttpContextAccessor httpContextAccessor
            , UserManager<ApplicationUser> userManager
            , IMemoryCache memoryCache)
        {
            _readWorkoutsService = readWorkoutsService;
            _manageWorkoutsService = manageWorkoutsService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _memoryCache = memoryCache;
        }

        public IActionResult Index(int? crossfitterWorkoutId)
        {
            //TODO: Add check rights!
            if (crossfitterWorkoutId.HasValue) // AND HAS RIGHTS!
            {
                string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                CrossfitterWorkout crossfitterWorkout = _readWorkoutsService.GetCrossfitterWorkout(userId, crossfitterWorkoutId.Value);
                ToLogWorkoutViewModel toLogWorkoutViewModel = _mapper.Map<ToLogWorkoutViewModel>(crossfitterWorkout);
                ViewBag.toLogWorkoutViewModel = toLogWorkoutViewModel;
                return View();
            }
//            ViewBag.crossfitterWorkoutId = crossfitterWorkoutId;
            return View();
        }

        #endregion

        #region methods

        /// <summary>
        ///     Get available crossfitter's workouts
        /// </summary>
        /// <returns>All available workouts</returns>
        [HttpGet]
        [Route("api/getAllCrossfittersWorkouts")]
        public List<ToLogWorkoutViewModel> GetAllCrossfittersWorkouts(int page = 1, int pageSize = 30, string userId = null, int? exerciseId = null)
        {
            List<ToLogWorkoutViewModel> crossfitersWorkouts = _memoryCache.GetOrCreate(_allMainpageResultsConst, entry =>
            {
                string userIdForWorkouts = userId;
                List<ToLogWorkoutViewModel> allResults = _readWorkoutsService
                    .GetAllCrossfittersWorkouts(userIdForWorkouts, exerciseId, page, pageSize)
                    .Select(_mapper.Map<ToLogWorkoutViewModel>)
                    .ToList();
                return allResults;
            });

            
            return crossfitersWorkouts;
        }

        /// <summary>
        /// Remove workout.
        /// </summary>
        /// <param name="crossfitterWorkoutId">
        /// The crossfitter workout id.
        /// </param>
        [HttpDelete]
        [Route("api/removeWorkout/{crossfitterWorkoutId}")]
        public void RemoveWorkout(int crossfitterWorkoutId)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            _memoryCache.Remove(_allMainpageResultsConst);
            //TODO: Add check rights!

            _manageWorkoutsService.RemoveWorkout(crossfitterWorkoutId, userId);
        }


        /// <summary>
        ///     Create and log workout
        /// </summary>
        /// <param name="model">Complex model with two properties: new workout and log workout models</param>
        [HttpPost]
        [Route("api/createAndLogNewWorkout")]
        public async Task CreateAndLogNewWorkout([FromBody] ToCreateAndLogNewWorkoutViewModel model)
        {

            try
            {
                ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
                CrossfitterWorkout crossfitterWorkout = _mapper.Map<CrossfitterWorkout>(model.LogWorkoutViewModel);
                crossfitterWorkout.Crossfitter = user;
                RoutineComplex newWorkoutRoutine = _mapper.Map<RoutineComplex>(model.NewWorkoutViewModel);
                newWorkoutRoutine.CreatedBy = user;
                _manageWorkoutsService.CreateAndLogNewWorkout(newWorkoutRoutine, crossfitterWorkout, user);
                _memoryCache.Remove(_allMainpageResultsConst);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }


        //
        //        /// <summary>
        //        /// Get person logging info by crossfit workout id
        //        /// </summary>
        //        /// <param name="preselectedCrossfitterWorkoutId">
        //        /// The preselected crossfitter workout id.
        //        /// </param>
        //        /// <returns>
        //        /// The <see cref="IHttpActionResult"/>.
        //        /// </returns>
        //        [HttpGet]
        //        [Route("getPersonLoggingInfo/{preselectedCrossfitterWorkoutId}")]
        //        public ToLogWorkoutViewModel GetPersonLoggingInfo(int preselectedCrossfitterWorkoutId)
        //        {
        //            CrossfitterWorkout crossfitterWorkout = _readWorkoutsService.GetCrossfitterWorkout(preselectedCrossfitterWorkoutId);
        //            var toLogWorkoutViewModel = Mapper.Map<ToLogWorkoutViewModel>(crossfitterWorkout);
        //            return toLogWorkoutViewModel;
        //        }

        #endregion
    }
}
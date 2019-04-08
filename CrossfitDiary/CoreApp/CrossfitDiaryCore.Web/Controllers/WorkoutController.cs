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
        private readonly ReadWorkoutsService _readWorkoutsService;
        private readonly ReadUsersService _readUsersService;
        private readonly ManageWorkoutsService _manageWorkoutsService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMemoryCache _memoryCache;
            
        private string _allMainpageResultsConst = "all-mainpage-results";
        private string _plannedWorkouts = "planned-workouts";

        public WorkoutController(ReadWorkoutsService readWorkoutsService
            , ReadUsersService readUsersService
            , ManageWorkoutsService manageWorkoutsService
            , IMapper mapper
            , IHttpContextAccessor httpContextAccessor
            , UserManager<ApplicationUser> userManager
            , IMemoryCache memoryCache)
        {
            _readWorkoutsService = readWorkoutsService;
            _readUsersService = readUsersService;
            _manageWorkoutsService = manageWorkoutsService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _memoryCache = memoryCache;
        }

        public IActionResult Index(int? crossfitterWorkoutId, int? workoutId)
        {
            //TODO: Add check rights!
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.canUserPlanWorkouts = _readUsersService.CanUserPlanWorkouts(userId);
            if (crossfitterWorkoutId.HasValue) // AND HAS RIGHTS!
            {
                CrossfitterWorkout crossfitterWorkout = _readWorkoutsService.GetCrossfitterWorkout(userId, crossfitterWorkoutId.Value);
                if (crossfitterWorkout == null)
                {
                    return View();
                }
                ToLogWorkoutViewModel toLogWorkoutViewModel = _mapper.Map<ToLogWorkoutViewModel>(crossfitterWorkout);
                ViewBag.toLogWorkoutViewModel = toLogWorkoutViewModel;
                return View();
            }

            if (!workoutId.HasValue)
            {
                return View();
            }

            RoutineComplex routineComplex = _readWorkoutsService.GetWorkout(workoutId.Value);
            if (routineComplex == null)
            {
                return View();
            }

            WorkoutViewModel workoutViewModel = _mapper.Map<WorkoutViewModel>(routineComplex);
            ViewBag.workoutViewModel = workoutViewModel;

            return View();
        }

        /// <summary>
        ///     Get available crossfitter's workouts
        /// </summary>
        /// <returns>All available workouts</returns>
        [HttpGet]
        [Route("api/getAllCrossfittersWorkouts")]
        public  List<ToLogWorkoutViewModel> GetAllCrossfittersWorkoutsAsync(int page = 1, int pageSize = 50)
        {
//            List<ToLogWorkoutViewModel> crossfitersWorkouts = await _memoryCache.GetOrCreate(_allMainpageResultsConst, async entry =>
             {
                 string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                 
                 List<CrossfitterWorkout> crossfitterWorkouts = _readWorkoutsService.GetAllCrossfittersWorkouts(userId, page, pageSize);
                 List<ToLogWorkoutViewModel> allResults = crossfitterWorkouts
                     .Select(_mapper.Map<ToLogWorkoutViewModel>)
                     .ToList();
                 return allResults;
             }
//            );


//            return crossfitersWorkouts;
        }

        /// <summary>
        ///     Get planned workouts
        /// </summary>
        /// <returns>All available workouts to do</returns>
        [HttpGet]
        [Route("api/getPlannedWorkoutsForToday")]
        public List<WorkoutViewModel> GetPlannedWorkoutsForToday()
        {
//            List<WorkoutViewModel> workoutViewModels = _memoryCache.GetOrCreate(_plannedWorkouts,  entry =>
//                {
                    List<RoutineComplex> workouts =  _readWorkoutsService.GetPlannedWorkouts(DateTime.Today);
                    List<WorkoutViewModel> allResults = workouts
                        .Select(_mapper.Map<WorkoutViewModel>)
                        .ToList();

                    return allResults.OrderBy(x => x.PlanningWorkoutLevel).ToList();
//                }
//            );


//            return workoutViewModels;
        }

        /// <summary>
        ///     Get planned workouts
        /// </summary>
        /// <returns>All available workouts to do</returns>
        [HttpGet]
        [Route("api/getLeaderboardByWorkout")]
        public List<LeaderboardItemViewModel> GetLeaderboardByWorkout(int crossfitterWorkoutId)
        {
            List<LeaderboardItemModel> leaderboardItemModels = _readWorkoutsService.GetLeaderboardByWorkout(crossfitterWorkoutId);
            return _mapper.Map<List<LeaderboardItemViewModel>>(leaderboardItemModels);
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
            
//            _memoryCache.Remove(_allMainpageResultsConst);
            //TODO: Add check rights!
            _manageWorkoutsService.RemoveWorkoutResult(crossfitterWorkoutId, userId);
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
//                _memoryCache.Remove(_allMainpageResultsConst);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        /// <summary>
        ///     Log workout from main page
        /// </summary>
        /// <param name="logWorkoutViewModel">To log workout view model</param>
        [HttpPost]
        [Route("api/quickLogWorkout")]
        public async Task QuickLogWorkout([FromBody] ToLogWorkoutViewModel logWorkoutViewModel)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            CrossfitterWorkout crossfitterWorkout = _mapper.Map<CrossfitterWorkout>(logWorkoutViewModel);
            crossfitterWorkout.Crossfitter = user;
            _manageWorkoutsService.LogNewWorkout(crossfitterWorkout);
//            _memoryCache.Remove(_allMainpageResultsConst);
        }

        /// <summary>
        ///     Create and log workout
        /// </summary>
        /// <param name="workoutViewModel">Complex model with two properties: new workout and log workout models</param>
        [HttpPost]
        [Route("api/createAndPlanWorkout")]
        public async Task CreateAndPlanWorkout([FromBody] WorkoutViewModel workoutViewModel)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            RoutineComplex newWorkoutRoutine = _mapper.Map<RoutineComplex>(workoutViewModel);
            newWorkoutRoutine.CreatedBy = user;
            _manageWorkoutsService.PlanWorkout(newWorkoutRoutine, user);
//            _memoryCache.Remove(_allMainpageResultsConst);
//            _memoryCache.Remove(_plannedWorkouts);
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
    }
}
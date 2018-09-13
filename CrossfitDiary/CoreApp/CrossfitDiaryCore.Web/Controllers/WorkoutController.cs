using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using CrossfitDiaryCore.BL.Services;
using CrossfitDiaryCore.Model;
using CrossfitDiaryCore.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace CrossfitDiary.Web.Api
{
    [Authorize]
    [Route("api")]
    public class WorkoutController : Controller
    {
        #region members
        private readonly ReadWorkoutsService _readWorkoutsService;
        private readonly ManageWorkoutsService _manageWorkoutsService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region constructors

        public WorkoutController(ReadWorkoutsService readWorkoutsService, ManageWorkoutsService manageWorkoutsService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _readWorkoutsService = readWorkoutsService;
            _manageWorkoutsService = manageWorkoutsService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region methods

        /// <summary>
        ///     Get available crossfitter's workouts
        /// </summary>
        /// <returns>All available workouts</returns>
        [HttpGet]
        [Route("getAllCrossfittersWorkouts")]
        public List<ToLogWorkoutViewModel> GetAllCrossfittersWorkouts(int page = 1, int pageSize = 30, string userId = null, int? exerciseId = null)
        {
            string userIdForWorkouts = userId;

            List<ToLogWorkoutViewModel> crossfitersWorkouts = _readWorkoutsService
                .GetAllCrossfittersWorkouts(userIdForWorkouts, exerciseId, page, pageSize)
                .Select(_mapper.Map<ToLogWorkoutViewModel>)
                .ToList();
            return crossfitersWorkouts;
        }

        /// <summary>
        /// Remove workout.
        /// </summary>
        /// <param name="crossfitterWorkoutId">
        /// The crossfitter workout id.
        /// </param>
        [HttpDelete]
        [Route("removeWorkout/{crossfitterWorkoutId}")]
        public void RemoveWorkout(int crossfitterWorkoutId)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            _manageWorkoutsService.RemoveWorkout(crossfitterWorkoutId, userId);
        }

        //
        //        /// <summary>
        //        ///     Create and log workout
        //        /// </summary>
        //        /// <param name="model">Complex model with two properties: new workout and log workout models</param>
        //        [HttpPost]
        //        [Route("createAndLogNewWorkout")]
        //        public void CreateAndLogNewWorkout(ToCreateAndLogNewWorkoutViewModel model)
        //        {
        //            
        //            ApplicationUser user = _applicationUserManager.FindById(HttpContext.Current.User.Identity.GetUserId());
        //            CrossfitterWorkout crossfitterWorkout = Mapper.Map<CrossfitterWorkout>(model.LogWorkoutViewModel);
        //            crossfitterWorkout.Crossfitter = user;
        //            RoutineComplex newWorkoutRoutine = Mapper.Map<RoutineComplex>(model.NewWorkoutViewModel);
        //            newWorkoutRoutine.CreatedBy = user;
        //
        //            _manageWorkoutsService.CreateAndLogNewWorkout(newWorkoutRoutine, crossfitterWorkout, model.LogWorkoutViewModel.IsEditMode);
        //        }
        //

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
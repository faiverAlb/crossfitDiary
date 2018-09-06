using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CrossfitDiaryCore.BL.Services;
using CrossfitDiaryCore.Model;
using CrossfitDiaryCore.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace CrossfitDiary.Web.Api
{
    [Authorize]
    [Route("api")]
    public class WorkoutController : Controller
    {
        #region members

//        private readonly ManageWorkoutsService _manageWorkoutsService;
        private readonly ReadWorkoutsService _readWorkoutsService;

        private readonly IMapper _mapper;
//        private readonly ApplicationUserManager _applicationUserManager;

        #endregion

        #region constructors

        public WorkoutController(ReadWorkoutsService readWorkoutsService, IMapper mapper)
        {
//            _manageWorkoutsService = manageWorkoutsService;
            _readWorkoutsService = readWorkoutsService;
            _mapper = mapper;
//            _applicationUserManager = applicationUserManager;
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
//        /// <summary>
//        /// Remove workout.
//        /// </summary>
//        /// <param name="crossfitterWorkoutId">
//        /// The crossfitter workout id.
//        /// </param>
//        [HttpDelete]
//        [Route("removeWorkout/{crossfitterWorkoutId}")]
//        public void RemoveWorkout(int crossfitterWorkoutId)
//        {
//            ApplicationUser user = _applicationUserManager.FindById(HttpContext.Current.User.Identity.GetUserId());
//            _readWorkoutsService.RemoveWorkout(crossfitterWorkoutId, user);
//        }
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using CrossfitDiary.Model;
using CrossfitDiary.Service;
using CrossfitDiary.Service.Interfaces;
using CrossfitDiary.Web.Configuration;
using CrossfitDiary.Web.ViewModels;
using Microsoft.AspNet.Identity;

namespace CrossfitDiary.Web.Api
{
    [Authorize]
    [RoutePrefix("api")]
    public class WorkoutController : BaseApiController
    {
        #region members

        private readonly ManageWorkoutsService _manageWorkoutsService;
        private readonly ReadWorkoutsService _readWorkoutsService;
        private readonly ApplicationUserManager _applicationUserManager;

        #endregion

        #region constructors

        public WorkoutController(ManageWorkoutsService manageWorkoutsService, ReadWorkoutsService readWorkoutsService, ApplicationUserManager applicationUserManager, IWorkoutService workoutService)
        {
            _manageWorkoutsService = manageWorkoutsService;
            _readWorkoutsService = readWorkoutsService;
            _applicationUserManager = applicationUserManager;
        }

        #endregion

        #region methods

        /// <summary>
        ///     Get available crossfitter's workouts
        /// </summary>
        /// <returns>All available workouts</returns>
        [HttpGet]
        [Route("getAllCrossfittersWorkouts")]
        public IHttpActionResult GetAllCrossfittersWorkouts(int page, int pageSize, string userId = null, int? exerciseId = null)
        {
            string userIdForWorkouts = exerciseId.HasValue && string.IsNullOrEmpty(userId) ? User.Identity.GetUserId() : userId;

            List<ToLogWorkoutViewModel> crossfitersWorkouts = _readWorkoutsService
                .GetAllCrossfittersWorkouts(userIdForWorkouts, exerciseId, page, pageSize)
                .Select(Mapper.Map<ToLogWorkoutViewModel>)
                .ToList();
            return Ok(crossfitersWorkouts);
        }


        /// <summary>
        ///     Create and log workout
        /// </summary>
        /// <param name="model">Complex model with two properties: new workout and log workout models</param>
        [HttpPost]
        [Route("createAndLogNewWorkout")]
        public void CreateAndLogNewWorkout(ToCreateAndLogNewWorkoutViewModel model)
        {
            
            ApplicationUser user = _applicationUserManager.FindById(HttpContext.Current.User.Identity.GetUserId());
            CrossfitterWorkout crossfitterWorkout = Mapper.Map<CrossfitterWorkout>(model.LogWorkoutViewModel);
            crossfitterWorkout.Crossfitter = user;
            RoutineComplex newWorkoutRoutine = Mapper.Map<RoutineComplex>(model.NewWorkoutViewModel);
            newWorkoutRoutine.CreatedBy = user;

            _manageWorkoutsService.CreateAndLogNewWorkout(newWorkoutRoutine, crossfitterWorkout, model.LogWorkoutViewModel.IsEditMode);
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
            ApplicationUser user = _applicationUserManager.FindById(HttpContext.Current.User.Identity.GetUserId());
            _readWorkoutsService.RemoveWorkout(crossfitterWorkoutId, user);
        }

        /// <summary>
        /// Get person logging info by crossfit workout id
        /// </summary>
        /// <param name="preselectedCrossfitterWorkoutId">
        /// The preselected crossfitter workout id.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [HttpGet]
        [Route("getPersonLoggingInfo/{preselectedCrossfitterWorkoutId}")]
        public IHttpActionResult GetPersonLoggingInfo(int preselectedCrossfitterWorkoutId)
        {
            CrossfitterWorkout crossfitterWorkout = _readWorkoutsService.GetCrossfitterWorkout(preselectedCrossfitterWorkoutId);
            return Ok(Mapper.Map<ToLogWorkoutViewModel>(crossfitterWorkout));
        }

        #endregion
    }
}
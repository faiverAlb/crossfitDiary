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

        private readonly CrossfitterService _crossfitterService;
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly IWorkoutService _workoutService;

        #endregion

        #region constructors

        public WorkoutController(CrossfitterService crossfitterService, ApplicationUserManager applicationUserManager,
            IWorkoutService workoutService)
        {
            _crossfitterService = crossfitterService;
            _applicationUserManager = applicationUserManager;
            _workoutService = workoutService;
        }

        #endregion

        #region methods

        /// <summary>
        ///     Get available workouts
        /// </summary>
        /// <returns>All available workouts</returns>
        [HttpGet]
        [Route("getAvailableWorkouts")]
        public IHttpActionResult GetAvailableWorkouts()
        {
            ApplicationUser user = _applicationUserManager.FindById(HttpContext.Current.User.Identity.GetUserId());
            IEnumerable<WorkoutViewModel> availableWorkouts = Mapper.Map<IEnumerable<WorkoutViewModel>>(_workoutService.GetDefaultAndUserWorkouts(user.Id));
            return Ok(availableWorkouts.OrderBy(x => x.DetailedTitle));
        }


        /// <summary>
        ///     Create workout by viewmodel
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [Route("createWorkout")]
        public void CreateWorkout(WorkoutViewModel model)
        {
            ApplicationUser user = _applicationUserManager.FindById(HttpContext.Current.User.Identity.GetUserId());

            RoutineComplex routineComplexToSave = Mapper.Map<RoutineComplex>(model);
            routineComplexToSave.CreatedBy = user;
            _crossfitterService.CreateWorkout(routineComplexToSave);
        }

        /// <summary>
        ///     Log workout
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [Route("logWorkout")]
        public void LogWorkout(ToLogWorkoutViewModel model)
        {
            var user = _applicationUserManager.FindById(HttpContext.Current.User.Identity.GetUserId());

            var crossfitterWorkout = Mapper.Map<CrossfitterWorkout>(model);
            crossfitterWorkout.Crossfitter = user;
            _crossfitterService.LogWorkout(crossfitterWorkout, model.IsEditMode);
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

            _crossfitterService.CreateAndLogNewWorkout(newWorkoutRoutine, crossfitterWorkout, model.LogWorkoutViewModel.IsEditMode);
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
            _crossfitterService.RemoveWorkout(crossfitterWorkoutId, user);
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
            CrossfitterWorkout crossfitterWorkout = _crossfitterService.GetCrossfitterWorkout(preselectedCrossfitterWorkoutId);
            return Ok(Mapper.Map<ToLogWorkoutViewModel>(crossfitterWorkout));
        }

        #endregion
    }
}
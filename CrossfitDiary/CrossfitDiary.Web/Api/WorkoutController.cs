using System;
using System.Collections.Generic;
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
            IEnumerable<WorkoutViewModel> availableWorkouts = Mapper.Map<IEnumerable<WorkoutViewModel>>(_workoutService.GetAvailableWorkouts());
            return Ok(availableWorkouts);
        }


        /// <summary>
        ///     Create workout by viewmodel
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [Route("createWorkout")]
        public void CreateWorkout(WorkoutViewModel model)
        {
            _crossfitterService.CreateWorkout(Mapper.Map<RoutineComplex>(model));
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
            _crossfitterService.LogWorkout(crossfitterWorkout);
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
            _crossfitterService.CreateAndLogNewWorkout(Mapper.Map<RoutineComplex>(model.NewWorkoutViewModel),
                crossfitterWorkout);
        }

        [HttpDelete]
        [Route("removeWorkout/{crossfitterWorkoutId}")]
        public void RemoveWorkout(int crossfitterWorkoutId)
        {
            _crossfitterService.RemoveWorkout(crossfitterWorkoutId);
        }

        #endregion
    }
}
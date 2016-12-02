using System.Web;
using System.Web.Http;
using AutoMapper;
using CrossfitDiary.Model;
using CrossfitDiary.Service.Interfaces;
using CrossfitDiary.Web.Configuration;
using CrossfitDiary.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CrossfitDiary.Web.Api
{

    [Authorize]
    [RoutePrefix("api")]
    public class WorkoutController : ApiController
    {
        private readonly ICrossfitterService _crossfitterService;

        public WorkoutController(ICrossfitterService crossfitterService)
        {
            _crossfitterService = crossfitterService;
        }

        /// <summary>
        /// Create workout by viewmodel
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [Route("createWorkout")]
        public void CreateWorkout(WorkoutViewModel model)
        {
            _crossfitterService.CreateWorkout(Mapper.Map<RoutineComplex>(model));
        }

        /// <summary>
        /// Log workout
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [Route("logWorkout")]
        public void LogWorkout(ToLogWorkoutViewModel model)
        {
            ApplicationUser user = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(HttpContext.Current.User.Identity.GetUserId());

            var crossfitterWorkout = Mapper.Map<CrossfitterWorkout>(model);
            crossfitterWorkout.Crossfitter = user;
            _crossfitterService.LogWorkout(crossfitterWorkout);
        }
    }

    
}

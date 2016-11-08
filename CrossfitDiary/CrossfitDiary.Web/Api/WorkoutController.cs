using System.Web.Http;
using CrossfitDiary.Service.Interfaces;
using CrossfitDiary.Web.ViewModels;

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
            _crossfitterService.CreateWorkout();
        }
    }

    
}

using System.Web.Http;

namespace CrossfitDiary.Web.Api
{

    [Authorize]
    [RoutePrefix("api")]
    public class WorkoutController : ApiController
    {

        /// <summary>
        /// Create workout by viewmodel
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [Route("createWorkout")]
        public void CreateWorkout(WorkoutViewModel model)
        {
            var test = 123;
        }
    }

    public class WorkoutViewModel
    {
        public string Title { get; set; }
    }
}

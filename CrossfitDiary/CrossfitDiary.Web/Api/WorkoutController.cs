using System;
using System.Collections.Generic;
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
        public int? RoundsCount { get; set; }

        //TODO: Possible problems with time string from js: 31:23 - will not be passed
        public TimeSpan? TimeToWork { get; set; }
        public TimeSpan? RestBetweenExercises { get; set; }
        public TimeSpan? RestBetweenRounds { get; set; }


        public List<ExerciseToDoViewModel> ExercisesToDoList { get; set; }
    }

    public class ExerciseToDoViewModel
    {
    }
}

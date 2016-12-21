using System;
using System.Collections.Generic;
using System.Linq;
using CrossfitDiary.Web.MvcHelpers;
using Newtonsoft.Json;

namespace CrossfitDiary.Web.ViewModels
{
    public class WorkoutViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("roundsCount")]
        public int? RoundsCount { get; set; }

        //TODO: Possible problems with time string from js, for example: 31:23 - will not be passed
        [JsonProperty("timeToWork")]
        public string TimeToWork { get; set; }

        [JsonProperty("restBetweenExercises")]
        public TimeSpan? RestBetweenExercises { get; set; }

        [JsonProperty("restBetweenRounds")]
        public TimeSpan? RestBetweenRounds { get; set; }

        [JsonProperty("workoutTypeViewModel")]
        public WorkoutTypeViewModel WorkoutTypeViewModel { get; set; }


        [JsonProperty("selectedWorkoutType")]
        public KeyValuePair<string, int> WorkoutTypeKeyValue
        {
            get
            {
                return EnumHelper.ToList(typeof (WorkoutTypeViewModel)).Single(x => x.Value == (int) WorkoutTypeViewModel);
            }
        }

        [JsonProperty("exercisesToDoList")]
        public List<ExerciseViewModel> ExercisesToDoList { get; set; }
    }
}
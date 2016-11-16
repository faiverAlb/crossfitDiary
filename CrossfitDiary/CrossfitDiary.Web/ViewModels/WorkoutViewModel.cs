using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CrossfitDiary.Web.ViewModels
{
    public class WorkoutViewModel
    {
        public string Title { get; set; }
        public int? RoundsCount { get; set; }

        //TODO: Possible problems with time string from js: 31:23 - will not be passed
        public TimeSpan? TimeToWork { get; set; }
        public TimeSpan? RestBetweenExercises { get; set; }
        public TimeSpan? RestBetweenRounds { get; set; }

        [JsonProperty("workoutTypeViewModel")]
        public WorkoutTypeViewModel WorkoutTypeViewModel { get; set; }

        [JsonProperty("exercisesToDoList")]
        public List<ExerciseViewModel> ExercisesToDoList { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using CrossfitDiary.Model;
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

        [JsonProperty("timeToWork")]
        public string TimeToWork { get; set; }

        [JsonProperty("restBetweenExercises")]
        public string RestBetweenExercises { get; set; }

        [JsonProperty("restBetweenRounds")]
        public string RestBetweenRounds { get; set; }

        [JsonProperty("workoutType")]
        public RoutineComplexType WorkoutType { get; set; }


        [JsonProperty("exercisesToDoList")]
        public List<ExerciseViewModel> ExercisesToDoList { get; set; }
    }
}
using System.Collections.Generic;
using CrossfitDiary.Model;
using Newtonsoft.Json;

namespace CrossfitDiary.Web.ViewModels
{
    public class WorkoutViewModel
    {
        #region properties

        [JsonProperty("detailedTitle")]
        public string DetailedTitle { get; set; }


        [JsonProperty("exercisesToDoList")]
        public List<ExerciseViewModel> ExercisesToDoList { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("restBetweenExercises")]
        public string RestBetweenExercises { get; set; }

        [JsonProperty("restBetweenRounds")]
        public string RestBetweenRounds { get; set; }

        [JsonProperty("roundsCount")]
        public int? RoundsCount { get; set; }

        [JsonProperty("timeToWork")]
        public string TimeToWork { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("workoutType")]
        public RoutineComplexType WorkoutType { get; set; }

        #endregion
    }
}
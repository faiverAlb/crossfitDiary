using System;
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

        [JsonProperty("timeCap")]
        public string TimeCap { get; set; }

        [JsonProperty("timeToWork")]
        public string TimeToWork { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("workoutType")]
        public RoutineComplexType WorkoutType { get; set; }

        [JsonProperty("workoutTypeDisplay")]
        public string WorkoutTypeDisplay
        {
            get
            {
                string displayValue = string.Empty;
                switch (WorkoutType)
                {
                    case RoutineComplexType.ForTime:
                        displayValue = "For time";
                        break;
                    case RoutineComplexType.AMRAP:
                        displayValue = "AMRAP";
                        break;
                    case RoutineComplexType.EMOM:
                        displayValue = "EMOM";
                        break;
                    case RoutineComplexType.E2MOM:
                        break;
                    case RoutineComplexType.NotForTime:
                        displayValue = "Not for time";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return displayValue;
            }
        }

        #endregion
    }
}
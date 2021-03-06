using System;
using System.Collections.Generic;
using CrossfitDiaryCore.Model;
using Newtonsoft.Json;

namespace CrossfitDiaryCore.Web.ViewModels
{
    public class WorkoutViewModel
    {
        [JsonProperty("children")]
        public ICollection<WorkoutViewModel> Children { get; set; }

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
                string displayValue;
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
                        displayValue = "E2MOM";
                        break;
                    case RoutineComplexType.NotForTime:
                        displayValue = "Not for time";
                        break;
                    case RoutineComplexType.ForTimeManyInners:
                        displayValue = "Complex for time";
                        break;
                    case RoutineComplexType.AMRAPN:
                        displayValue = "Complex amrap";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return displayValue;
            }
        }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("resultsCount")]
        public int ResultsCount { get; set; }

        [JsonProperty("canSeeLeaderboard")]
        public bool CanSeeLeaderboard { get; set; }


        [JsonProperty("asNonBreakingSet")] 
        public bool AsNonBreakingSet { get; set; }
        
        
        [JsonProperty("findMaxWeight")] 
        public bool FindMaxWeight { get; set; }



    }
}
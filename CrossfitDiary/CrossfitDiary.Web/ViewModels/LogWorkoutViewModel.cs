using System.Collections.Generic;
using Newtonsoft.Json;

namespace CrossfitDiary.Web.ViewModels
{
    public class LogWorkoutViewModel
    {
        [JsonProperty("availableWorkouts")]
        public IEnumerable<WorkoutViewModel> AvailableWorkouts { get; set; }
    }
}
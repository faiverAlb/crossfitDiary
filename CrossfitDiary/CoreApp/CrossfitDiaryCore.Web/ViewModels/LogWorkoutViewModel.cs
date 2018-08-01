using System.Collections.Generic;
using Newtonsoft.Json;

namespace CrossfitDiaryCore.Web.ViewModels
{
    public class LogWorkoutViewModel
    {
        [JsonProperty("availableWorkouts")]
        public IEnumerable<WorkoutViewModel> AvailableWorkouts { get; set; }
    }
}
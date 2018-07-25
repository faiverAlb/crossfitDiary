using Newtonsoft.Json;

namespace CrossfitDiaryCore.Web.ViewModels
{
    public class ToCreateAndLogNewWorkoutViewModel
    {
        [JsonProperty("newWorkoutViewModel")]
        public WorkoutViewModel NewWorkoutViewModel { get; set; }

        [JsonProperty("logWorkoutViewModel")]
        public ToLogWorkoutViewModel LogWorkoutViewModel { get; set; }
    }
}
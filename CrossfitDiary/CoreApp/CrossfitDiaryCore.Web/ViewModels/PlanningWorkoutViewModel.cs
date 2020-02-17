using Newtonsoft.Json;

namespace CrossfitDiaryCore.Web.ViewModels
{
    public class PlanningWorkoutViewModel
    {
        [JsonProperty("id")]
        public virtual int Id { get; set; }
        
        [JsonProperty("planningWorkoutLevel")]
        public PlanningWorkoutLevel? PlanningWorkoutLevel { get; set; }

        [JsonProperty("wodSubType")]
        public WodSubType WodSubType { get; set; }

        [JsonProperty("displayPlanDate")]
        public string DisplayPlanDate { get; set; }
        
        [JsonProperty("workoutViewModel")]
        public WorkoutViewModel WorkoutViewModel { get; set; }

        [JsonProperty("isPrivatePlanning")]
        public bool IsPrivatePlanning { get; set; }

    }
}
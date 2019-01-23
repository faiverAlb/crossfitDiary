using System;
using Newtonsoft.Json;

namespace CrossfitDiaryCore.Web.ViewModels
{
    public class ToLogWorkoutViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("canBeRemovedByCurrentUser")]
        public bool CanBeRemovedByCurrentUser { get; set; }

        [JsonProperty("crossfitterWorkoutId")]
        public int CrossfitterWorkoutId { get; set; }

        [JsonConverter(typeof(DateFormatConverter), "dd.MM.yyyy")]
        public DateTime Date { get; set; }

        [JsonProperty("displayDate")]
        public string DisplayDate { get; set; }

        [JsonProperty("distance")]
        public int? Distance { get; set; }

        [JsonProperty("isEditMode")]
        public bool IsEditMode { get; set; }

        [JsonProperty("isPlanned")]
        public bool IsPlanned { get; set; }

        [JsonProperty("planningWorkoutLevel")]
        public PlanningWorkoutLevel? PlanningWorkoutLevel { get; set; }

        [JsonProperty("isRx")]
        public bool IsRx { get; set; }

        [JsonProperty("partialRepsFinished")]
        public int? PartialRepsFinished { get; set; }

        [JsonProperty("repsToFinishOnCapTime")]
        public int? RepsToFinishOnCapTime { get; set; }

        [JsonProperty("roundsFinished")]
        public int? RoundsFinished { get; set; }

        [JsonProperty("selectedWorkoutId")]
        public int SelectedWorkoutId { get; set; }

        // TODO: Move to separate view model?
        [JsonProperty("selectedWorkoutName")]
        public string SelectedWorkoutName { get; set; }

        [JsonProperty("timePassed")]
        public string TimePassed { get; set; }


        [JsonProperty("wasFinished")]
        public bool WasFinished { get; set; }

        [JsonProperty("workouterId")]
        public string WorkouterId { get; set; }

        [JsonProperty("workouterName")]
        public string WorkouterName { get; set; }

        [JsonProperty("workoutViewModel")]
        public WorkoutViewModel WorkoutViewModel { get; set; }

    }

    public enum PlanningWorkoutLevel
    {
        Scaled = 0,
        Rx = 1,
        RxPlus = 2
    }
}
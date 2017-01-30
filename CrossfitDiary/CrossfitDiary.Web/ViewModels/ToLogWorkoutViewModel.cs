using System;
using Newtonsoft.Json;

namespace CrossfitDiary.Web.ViewModels
{
    public class ToLogWorkoutViewModel
    {
        [JsonProperty("selectedWorkoutId")]
        public string SelectedWorkoutId { get; set; }

        [JsonProperty("roundsFinished")]
        public int? RoundsFinished { get; set; }

        [JsonProperty("partialRepsFinished")]
        public int? PartialRepsFinished { get; set; }

        [JsonProperty("timePassed")]
        public string TimePassed { get; set; }

        [JsonProperty("distance")]
        public int? Distance { get; set; }


        [JsonProperty("wasFinished")]
        public bool WasFinished { get; set; }

        [JsonProperty("isRx")]
        public bool IsRx { get; set; }

        //TODO: Move to separate view model?

        [JsonProperty("selectedWorkoutName")]
        public string SelectedWorkoutName { get; set; }

        [JsonProperty("measureDisplayName")]
        public string MeasureDisplayName { get; set; }
        
        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }
}
using Newtonsoft.Json;

namespace CrossfitDiaryCore.Web.ViewModels
{
    public class PersonMaximumViewModel
    {

        [JsonProperty("exerciseId")]
        public int ExerciseId { get; set; }

        [JsonProperty("maximumWeight")]
        public double? MaximumWeight { get; set; }

        [JsonProperty("maximumAlternativeWeight")]
        public double? MaximumAlternativeWeight { get; set; }
    }
}
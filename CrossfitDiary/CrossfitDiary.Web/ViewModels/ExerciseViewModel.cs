using System.Collections.Generic;
using Newtonsoft.Json;

namespace CrossfitDiary.Web.ViewModels
{
    public class ExerciseViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; } 

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("exerciseMeasures")]
        public IEnumerable<ExerciseMeasureViewModel> ExerciseMeasures { get; set; }

        [JsonProperty("isAlternative")]
        public bool IsAlternative { get; set; }
    }


    public class ExerciseMeasureViewModel
    {
        [JsonProperty("exerciseMeasureType")]
        public ExerciseMeasureTypeViewModel ExerciseMeasureType { get; set; }
    }

    public class ExerciseMeasureTypeViewModel
    {
        [JsonProperty("measureType")]
        public MeasureTypeViewModel MeasureType { get; set; }

        [JsonProperty("measureValue")]
        public string MeasureValue { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("shortMeasureDescription")]
        public string ShortMeasureDescription { get; set; }
    }

    public enum MeasureTypeViewModel
    {
        Distance = 0,
        Count = 1,
        Weight = 2
    }
}
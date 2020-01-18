using System.Collections.Generic;
using Newtonsoft.Json;

namespace CrossfitDiaryCore.Web.ViewModels
{
    public class ExerciseViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; } 
        
        [JsonProperty("abbreviation")]
        public string Abbreviation { get; set; }

        [JsonProperty("exerciseMeasures")]
        public IEnumerable<ExerciseMeasureViewModel> ExerciseMeasures { get; set; }

        [JsonProperty("isAlternative")]
        public bool IsAlternative { get; set; }

        [JsonProperty("isNewWeightMaximum")]
        public bool IsNewWeightMaximum { get; set; }

        [JsonProperty("isDoUnbroken")]
        public bool IsDoUnbroken { get; set; }

        [JsonProperty("addedToMaxWeightString")]
        public string AddedToMaxWeightString { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        public int? TempRoutineSimpleId { get; set; }

        [JsonProperty("weightDisplayType")]
        public WeightDisplayType WeightDisplayType { get; set; } = WeightDisplayType.PercentMaxPM;

    }


    public class ExerciseMeasureViewModel
    {
        [JsonProperty("measureType")]
        public MeasureTypeViewModel MeasureType { get; set; }

        [JsonProperty("measureValue")]
        public string MeasureValue { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("shortMeasureDescription")]
        public string ShortMeasureDescription { get; set; }

        [JsonProperty("isRequired")]
        public bool IsRequired { get; set; } = true;


    }

    public enum WeightDisplayType
    {
        Default = 0,
        PercentPreviousPM,
        PercentMaxPM = 2
    }


    public enum MeasureTypeViewModel
    {
        Distance = 1,
        Count = 2,
        Weight = 3,
        Calories = 4,
        Height = 5,
        AlternativeDistance = 6,
        AlternativeCount = 7,
        AlternativeWeight = 8,
        AlternativeCalories = 9,
        AlternativeHeight = 10,
    }
}
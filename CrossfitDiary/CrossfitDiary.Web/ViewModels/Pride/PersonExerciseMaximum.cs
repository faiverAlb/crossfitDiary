using Newtonsoft.Json;

namespace CrossfitDiary.Web.ViewModels.Pride
{
    /// <summary>
    ///     The person exercise maximum view model.
    /// </summary>
    public class PersonExerciseMaximumViewModel
    {
        #region properties

        /// <summary>
        ///     Gets or sets the date.
        /// </summary>
        [JsonProperty("date")]
        public string Date { get; set; }

        /// <summary>
        ///     Gets or sets the exercise title.
        /// </summary>
        [JsonProperty("exerciseDisplayName")]
        public string ExerciseDisplayName { get; set; }


        /// <summary>
        ///     Gets or sets the workout exercise Id.
        /// </summary>
        [JsonProperty("exerciseId")]
        public int ExerciseId { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is it me.
        /// </summary>
        [JsonProperty("isItMe")]
        public bool IsItMe { get; set; }

        /// <summary>
        ///     Gets or sets the maximum weight.
        /// </summary>
        [JsonProperty("maximumWeight")]
        public string MaximumWeight { get; set; }

        /// <summary>
        ///     Gets or sets the person name.
        /// </summary>
        [JsonProperty("personName")]
        public string PersonName { get; set; }

        /// <summary>
        ///     Gets or sets the position between others.
        /// </summary>
        [JsonProperty("positionBetweenOthers")]
        public int PositionBetweenOthers { get; set; }

        /// <summary>
        ///     Gets or sets the workout title.
        /// </summary>
        [JsonProperty("workoutTitle")]
        public string WorkoutTitle { get; set; }

        #endregion
    }
}
using Newtonsoft.Json;

namespace CrossfitDiary.Web.ViewModels.Pride
{
    /// <summary>
    /// The person exercise maximum view model.
    /// </summary>
    public class PersonExerciseMaximumViewModel
    {
        #region properties

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        [JsonProperty("date")]
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets the maximum weight.
        /// </summary>
        [JsonProperty("maximumWeight")]
        public string MaximumWeight { get; set; }

        /// <summary>
        /// Gets or sets the person name.
        /// </summary>
        [JsonProperty("personName")]
        public string PersonName { get; set; }

        #endregion
    }
}
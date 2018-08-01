using System.Collections.Generic;
using CrossfitDiaryCore.Web.ViewModels.Pride;
using Newtonsoft.Json;

namespace CrossfitDiaryCore.Web.ViewModels
{
    public class PersonDataViewModel
    {

        [JsonProperty("personMaximums")]
        public List<PersonExerciseMaximumViewModel> PersonMaximums { get; set; } = new List<PersonExerciseMaximumViewModel>();

        [JsonProperty("initialWorkouts")]
        public List<ToLogWorkoutViewModel> InitialWorkouts { get; set; }
    }
}
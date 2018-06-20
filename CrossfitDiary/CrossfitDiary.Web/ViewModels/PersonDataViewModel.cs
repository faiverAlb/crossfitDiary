using System;
using System.Collections.Generic;
using CrossfitDiary.Model;
using CrossfitDiary.Web.ViewModels.Pride;
using Newtonsoft.Json;

namespace CrossfitDiary.Web.ViewModels
{
    public class PersonDataViewModel
    {

        [JsonProperty("personMaximums")]
        public List<PersonExerciseMaximumViewModel> PersonMaximums { get; set; } = new List<PersonExerciseMaximumViewModel>();

        [JsonProperty("initialWorkouts")]
        public List<ToLogWorkoutViewModel> InitialWorkouts { get; set; }
    }
}
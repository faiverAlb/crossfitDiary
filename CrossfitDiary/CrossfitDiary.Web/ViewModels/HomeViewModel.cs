using System;
using System.Collections.Generic;
using CrossfitDiary.Model;
using CrossfitDiary.Web.ViewModels.Pride;
using Newtonsoft.Json;

namespace CrossfitDiary.Web.ViewModels
{
    public class HomeViewModel
    {
        [JsonProperty("allWorkouts")]
        public List<ToLogWorkoutViewModel> AllWorkouts { get; set; } = new List<ToLogWorkoutViewModel>();

        [JsonProperty("personMaximums")]
        public List<PersonExerciseMaximumViewModel> PersonMaximums { get; set; } = new List<PersonExerciseMaximumViewModel>();
    }

//    public class DayWorkoutViewModel
//    {
//
//        [JsonProperty("date")]
//        public DateTime Date { get; set; }
//
//        [JsonProperty("displayDate")]
//        public string DisplayDate => Date.ToString("d");
//
//        [JsonProperty("dayOfWeek")]
//        public string DayOfWeek { get; set; }
//
//        [JsonProperty("doneWorkouts")]
//        public List<ToLogWorkoutViewModel> DoneWorkouts { get; set; } = new List<ToLogWorkoutViewModel>();
//
//    }
}
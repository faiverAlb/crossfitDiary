using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CrossfitDiary.Web.ViewModels
{
    public class HomeViewModel
    {
        [JsonProperty("weekWorkouts")]
        public List<DayWorkoutViewModel> WeekWorkouts { get; set; } = new List<DayWorkoutViewModel>();
    }

    public class DayWorkoutViewModel
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("displayDate")]
        public string DisplayDate { get { return Date.ToString("d"); } }

        [JsonProperty("dayOfWeek")]
        public string DayOfWeek { get; set; }

        [JsonProperty("workouts")]
        public List<ToLogWorkoutViewModel> Workouts { get; set; } = new List<ToLogWorkoutViewModel>();

    }
}
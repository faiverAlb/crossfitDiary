using System;
using System.Collections.Generic;

namespace CrossfitDiary.Web.ViewModels
{
    public class HomeViewModel
    {
        public List<DayWorkoutViewModel> WeekWorkouts { get; set; } = new List<DayWorkoutViewModel>();
    }

    public class DayWorkoutViewModel
    {
        public DateTime Date { get; set; }

        public string DayOfWeek { get; set; }

        public List<ToLogWorkoutViewModel> Workouts { get; set; } = new List<ToLogWorkoutViewModel>();

    }
}
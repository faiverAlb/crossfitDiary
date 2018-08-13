using System;
using AutoMapper;
using CrossfitDiaryCore.Model;
using CrossfitDiaryCore.Web.ViewModels;

namespace CrossfitDiaryCore.Web.AutomapperConfiguration.Resolvers
{
    public class TimePassedResolver : IValueResolver<CrossfitterWorkout, ToLogWorkoutViewModel, string>
    {
        public string Resolve(CrossfitterWorkout source, ToLogWorkoutViewModel destination, string destMember, ResolutionContext context)
        {
            if (source.TimePassed.HasValue == false)
            {
                return null;
            }

            TimeSpan totalTime = source.TimePassed.Value;
            if (totalTime.TotalHours >= 1)
            {
                return totalTime.ToString();
            }

            string result = $"{totalTime.Minutes:00}:{totalTime.Seconds:00}";
            return result;

        }
    }
}
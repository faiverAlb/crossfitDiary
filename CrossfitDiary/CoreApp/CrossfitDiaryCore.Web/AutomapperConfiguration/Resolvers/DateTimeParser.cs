using System;
using System.Globalization;
using System.Linq;
using AutoMapper;
using CrossfitDiaryCore.Model;
using CrossfitDiaryCore.Web.ViewModels;

namespace CrossfitDiaryCore.Web.AutomapperConfiguration.Resolvers
{
    public class DateTimeParser : IValueResolver<ToLogWorkoutViewModel, CrossfitterWorkout, DateTime>, IValueResolver<WorkoutViewModel, RoutineComplex, DateTime?>
    {
        public DateTime Resolve(ToLogWorkoutViewModel source, CrossfitterWorkout destination, DateTime destMember,ResolutionContext context)
        {
            return ParseDate(source.DisplayDate);
            
        }

        public DateTime? Resolve(WorkoutViewModel source, RoutineComplex destination, DateTime? destMember, ResolutionContext context)
        {
            return ParseDate(source.DisplayPlanDate);
        }

        private DateTime ParseDate(string dateToParse)
        {
            var ci = new CultureInfo("ru-RU");
            var formats = new[] { "dd.MM.yyyy"}.Union(ci.DateTimeFormat.GetAllDateTimePatterns()).ToArray();

            var dateTime = DateTime.ParseExact(dateToParse, formats, ci, DateTimeStyles.AssumeLocal);
            return dateTime;
        }
    }
}
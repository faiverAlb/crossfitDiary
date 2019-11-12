using System;
using System.Globalization;
using System.Linq;
using AutoMapper;
using CrossfitDiaryCore.Model;
using CrossfitDiaryCore.Web.ViewModels;

namespace CrossfitDiaryCore.Web.AutomapperConfiguration.Resolvers
{
    public class DateTimeParser : IValueResolver<ToLogWorkoutViewModel, CrossfitterWorkout, DateTime>
    {
        public DateTime Resolve(ToLogWorkoutViewModel source, CrossfitterWorkout destination, DateTime destMember,ResolutionContext context)
        {
            var ci = new CultureInfo("en-US");
            var formats = new[] { "M-d-yyyy", "dd-MM-yyyy", "MM-dd-yyyy", "M.d.yyyy", "dd.MM.yyyy", "MM.dd.yyyy" }.Union(ci.DateTimeFormat.GetAllDateTimePatterns()).ToArray();

            var dateTime = DateTime.ParseExact(source.DisplayDate, formats, ci, DateTimeStyles.AssumeLocal);
            return dateTime;
        }
    }
}
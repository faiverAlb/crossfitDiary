using System;
using System.Globalization;
using System.Linq;
using AutoMapper;
using CrossfitDiaryCore.Model;
using CrossfitDiaryCore.Web.AutomapperConfiguration.Resolvers;
using CrossfitDiaryCore.Web.ViewModels;

namespace CrossfitDiaryCore.Web.AutomapperConfiguration
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName => nameof(ViewModelToDomainMappingProfile);

        public ViewModelToDomainMappingProfile()
        {
            CreateMap<MeasureTypeViewModel, MeasureType>();
            CreateMap<PlanningWorkoutViewModel, PlanningHistory>()
                .ForMember(x => x.RoutineComplex, x => x.MapFrom(y => y.WorkoutViewModel))
                .ForMember(x => x.PlanningDate, x => x.MapFrom<DateTimeParser>())
                .ForMember(x => x.PlanningLevel, x => x.MapFrom(y => y.PlanningWorkoutLevel));
            //CreateMap<ExerciseMeasureTypeViewModel, ExerciseMeasureType>();
            CreateMap<ExerciseMeasureViewModel, ExerciseMeasure>();

            CreateMap<WorkoutViewModel, RoutineComplex>()
                .ForMember(x => x.ComplexType, x => x.MapFrom(y => y.WorkoutType))
                .ForMember(x => x.RoutineSimple, x => x.MapFrom(y => y.ExercisesToDoList))
                .BeforeMap((model, complex) =>
                {
                        int index = 0;
                        foreach (ExerciseViewModel exerciseViewModel in model.ExercisesToDoList)
                        {
                            exerciseViewModel.Position = index++;
                        }
                    })
                .ForMember(x => x.RoundCount, x => x.MapFrom(y => y.RoundsCount))
                .ForMember(x => x.TimeToWork, x => x.MapFrom(y => ParseTimeSpanFromString(y.TimeToWork)))
                .ForMember(x => x.TimeCap, x => x.MapFrom(y => ParseTimeSpanFromString(y.TimeCap)))
                .ForMember(x => x.RestBetweenRounds, x => x.MapFrom(y => ParseTimeSpanFromString(y.RestBetweenRounds)))
                .ForMember(x => x.RestBetweenExercises, x => x.MapFrom(y => ParseTimeSpanFromString(y.RestBetweenExercises)));

            
            CreateMap<ExerciseViewModel, RoutineSimple>()
                .ForMember(x => x.ExerciseId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.WeightDisplayType, x => x.MapFrom(y => y.WeightDisplayType))
                .ForMember(x => x.WeightPercentValue, x => x.MapFrom(y => y.WeightPercentValue))
                .ForMember(x => x.IsDoUnbroken, x => x.MapFrom(y => y.IsDoUnbroken))
                .ForMember(x => x.Count, opt => opt.MapFrom<CountResolver>())
                .ForMember(x => x.Distance, opt => opt.MapFrom<DistanceResolver>())
                .ForMember(x => x.Weight, opt => opt.MapFrom<WeightResolver>())
                .ForMember(x => x.AlternativeWeight, opt => opt.MapFrom<AlternativeWeightResolver>())
                .ForMember(x => x.Calories, opt => opt.MapFrom<CaloriesResolver>())
                .ForMember(x => x.Centimeters, opt => opt.MapFrom<CentimetersResolver>())
                .ForMember(x => x.Seconds, opt => opt.MapFrom<SecondsResolver>());

            CreateMap<ToLogWorkoutViewModel, CrossfitterWorkout>()
                .ForMember(x => x.RoutineComplexId, x => x.MapFrom(y => y.SelectedWorkoutId))
                .ForMember(x => x.Id, x => x.MapFrom(y => y.CrossfitterWorkoutId))
                .ForMember(x => x.Date, x => x.MapFrom<DateTimeParser>())
                .ForMember(x => x.IsModified, x => x.MapFrom(y => !y.IsRx))
                .ForMember(x => x.TimePassed, x => x.MapFrom(y => ParseTimeSpanFromString(y.TimePassed)));
        }

        private TimeSpan? ParseTimeSpanFromString(string toParseTimeSpan)
        {
            if (string.IsNullOrEmpty(toParseTimeSpan))
            {
                return null;
            }
            TimeSpan timeSpan = new TimeSpan(0, int.Parse(toParseTimeSpan.Split(':')[0]), int.Parse(toParseTimeSpan.Split(':')[1]));
            return timeSpan;
        }
    }

    public static class DecimalParse
    {
        public static decimal ParseDecimal(string measureValue)
        {
//            return 0;
            measureValue = measureValue.Replace(',', '.');
            decimal decimalResult = 0;
            if (decimal.TryParse(measureValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimalResult))
            {
                return decimalResult;
            }
            return decimal.Parse(measureValue, NumberStyles.Any,CultureInfo.InvariantCulture);
        }
    }
    public class CountResolver : IValueResolver<ExerciseViewModel, RoutineSimple, decimal?>
    {
        public decimal? Resolve(ExerciseViewModel source, RoutineSimple destination, decimal? destMember, ResolutionContext context)
        {
            ExerciseMeasureViewModel measure = source.ExerciseMeasures.SingleOrDefault(x => x.MeasureType == MeasureTypeViewModel.Count);
            if (string.IsNullOrEmpty(measure?.MeasureValue))
            {
                return null;
            }

            return DecimalParse.ParseDecimal(measure.MeasureValue);
        }

    }

    public class DistanceResolver : IValueResolver<ExerciseViewModel, RoutineSimple, decimal?>
    {
        public decimal? Resolve(ExerciseViewModel source, RoutineSimple destination, decimal? destMember, ResolutionContext context)
        {
            ExerciseMeasureViewModel measure = source.ExerciseMeasures.SingleOrDefault(x => x.MeasureType == MeasureTypeViewModel.Distance);
            if (string.IsNullOrEmpty(measure?.MeasureValue))
            {
                return null;
            }

            return DecimalParse.ParseDecimal(measure.MeasureValue);
        }
    }

    public class WeightResolver : IValueResolver<ExerciseViewModel, RoutineSimple, decimal?>
    {
        public decimal? Resolve(ExerciseViewModel source, RoutineSimple destination, decimal? destMember, ResolutionContext context)
        {
            ExerciseMeasureViewModel measure = source.ExerciseMeasures.SingleOrDefault(x => x.MeasureType == MeasureTypeViewModel.Weight);
            if (string.IsNullOrEmpty(measure?.MeasureValue))
            {
                return null;
            }

            return DecimalParse.ParseDecimal(measure.MeasureValue);
        }
    }
    public class AlternativeWeightResolver : IValueResolver<ExerciseViewModel, RoutineSimple, decimal?>
    {
        public decimal? Resolve(ExerciseViewModel source, RoutineSimple destination, decimal? destMember, ResolutionContext context)
        {
            ExerciseMeasureViewModel measure = source.ExerciseMeasures.SingleOrDefault(x => x.MeasureType == MeasureTypeViewModel.AlternativeWeight);
            if (string.IsNullOrEmpty(measure?.MeasureValue))
            {
                return null;
            }

            return DecimalParse.ParseDecimal(measure.MeasureValue);
        }
    }
    public class CaloriesResolver : IValueResolver<ExerciseViewModel, RoutineSimple, decimal?>
    {
        public decimal? Resolve(ExerciseViewModel source, RoutineSimple destination, decimal? destMember, ResolutionContext context)
        {
            ExerciseMeasureViewModel measure = source.ExerciseMeasures.SingleOrDefault(x => x.MeasureType == MeasureTypeViewModel.Calories);
            if (string.IsNullOrEmpty(measure?.MeasureValue))
            {
                return null;
            }
            return DecimalParse.ParseDecimal(measure.MeasureValue);
        }
    }
    public class CentimetersResolver : IValueResolver<ExerciseViewModel, RoutineSimple, decimal?>
    {
        public decimal? Resolve(ExerciseViewModel source, RoutineSimple destination, decimal? destMember, ResolutionContext context)
        {
            ExerciseMeasureViewModel measure = source.ExerciseMeasures.SingleOrDefault(x => x.MeasureType == MeasureTypeViewModel.Height);
            if (string.IsNullOrEmpty(measure?.MeasureValue))
            {
                return null;
            }

            return DecimalParse.ParseDecimal(measure.MeasureValue);
        }
    }
    public class SecondsResolver : IValueResolver<ExerciseViewModel, RoutineSimple, int?>
    {
        public int? Resolve(ExerciseViewModel source, RoutineSimple destination, int? destMember, ResolutionContext context)
        {
            ExerciseMeasureViewModel measure = source.ExerciseMeasures.SingleOrDefault(x => x.MeasureType == MeasureTypeViewModel.Time);
            if (string.IsNullOrEmpty(measure?.MeasureValue))
            {
                return null;
            }

            if (int.TryParse(measure.MeasureValue, out var res))
            {
                return res;
            }
            return null;

        }
    }
}
using System;
using System.Globalization;
using System.Linq;
using AutoMapper;
using CrossfitDiaryCore.Model;
using CrossfitDiaryCore.Web.ViewModels;

namespace CrossfitDiaryCore.Web.AutomapperConfiguration
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName => nameof(ViewModelToDomainMappingProfile);

        public ViewModelToDomainMappingProfile()
        {
            CreateMap<MeasureTypeViewModel, MeasureType>();
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
                .ForMember(x => x.IsDoUnbroken, x => x.MapFrom(y => y.IsDoUnbroken))
                .ForMember(x => x.Count, opt => opt.ResolveUsing<CountResolver>())
                .ForMember(x => x.Distance, opt => opt.ResolveUsing<DistanceResolver>())
                .ForMember(x => x.Weight, opt => opt.ResolveUsing<WeightResolver>())
                .ForMember(x => x.Calories, opt => opt.ResolveUsing<CaloriesResolver>())
                .ForMember(x => x.Centimeters, opt => opt.ResolveUsing<CentimetersResolver>());

            CreateMap<ToLogWorkoutViewModel, CrossfitterWorkout>()
                .ForMember(x => x.RoutineComplexId, x => x.MapFrom(y => y.SelectedWorkoutId))
                .ForMember(x => x.Id, x => x.MapFrom(y => y.CrossfitterWorkoutId))
                .ForMember(x => x.Date, x => x.MapFrom(y => DateTime.Parse(y.DisplayDate)))
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
            return decimal.Parse(measureValue, NumberStyles.Any);
        }
    }
    public class CountResolver : IValueResolver<ExerciseViewModel, RoutineSimple, decimal?>
    {
        public decimal? Resolve(ExerciseViewModel source, RoutineSimple destination, decimal? destMember, ResolutionContext context)
        {
            ExerciseMeasureViewModel foundTimeMeasure = source.ExerciseMeasures.SingleOrDefault(x => x.MeasureType == MeasureTypeViewModel.Count);
            if (string.IsNullOrEmpty(foundTimeMeasure?.MeasureValue))
            {
                return null;
            }

            return DecimalParse.ParseDecimal(foundTimeMeasure.MeasureValue);
        }

    }

    public class DistanceResolver : IValueResolver<ExerciseViewModel, RoutineSimple, decimal?>
    {
        public decimal? Resolve(ExerciseViewModel source, RoutineSimple destination, decimal? destMember, ResolutionContext context)
        {
            ExerciseMeasureViewModel foundTimeMeasure = source.ExerciseMeasures.SingleOrDefault(x => x.MeasureType == MeasureTypeViewModel.Distance);
            if (string.IsNullOrEmpty(foundTimeMeasure?.MeasureValue))
            {
                return null;
            }

            return DecimalParse.ParseDecimal(foundTimeMeasure.MeasureValue);
        }
    }

    public class WeightResolver : IValueResolver<ExerciseViewModel, RoutineSimple, decimal?>
    {
        public decimal? Resolve(ExerciseViewModel source, RoutineSimple destination, decimal? destMember, ResolutionContext context)
        {
            ExerciseMeasureViewModel foundTimeMeasure = source.ExerciseMeasures.SingleOrDefault(x => x.MeasureType == MeasureTypeViewModel.Weight);
            if (string.IsNullOrEmpty(foundTimeMeasure?.MeasureValue))
            {
                return null;
            }

            return DecimalParse.ParseDecimal(foundTimeMeasure.MeasureValue);
        }
    }
    public class CaloriesResolver : IValueResolver<ExerciseViewModel, RoutineSimple, decimal?>
    {
        public decimal? Resolve(ExerciseViewModel source, RoutineSimple destination, decimal? destMember, ResolutionContext context)
        {
            ExerciseMeasureViewModel foundTimeMeasure = source.ExerciseMeasures.SingleOrDefault(x => x.MeasureType == MeasureTypeViewModel.Calories);
            if (string.IsNullOrEmpty(foundTimeMeasure?.MeasureValue))
            {
                return null;
            }
            return DecimalParse.ParseDecimal(foundTimeMeasure.MeasureValue);
        }
    }
    public class CentimetersResolver : IValueResolver<ExerciseViewModel, RoutineSimple, decimal?>
    {
        public decimal? Resolve(ExerciseViewModel source, RoutineSimple destination, decimal? destMember, ResolutionContext context)
        {
            ExerciseMeasureViewModel foundTimeMeasure = source.ExerciseMeasures.SingleOrDefault(x => x.MeasureType == MeasureTypeViewModel.Height);
            if (string.IsNullOrEmpty(foundTimeMeasure?.MeasureValue))
            {
                return null;
            }

            return DecimalParse.ParseDecimal(foundTimeMeasure.MeasureValue);
        }
    }
}
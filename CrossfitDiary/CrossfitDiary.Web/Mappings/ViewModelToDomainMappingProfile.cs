using System;
using System.Linq;
using AutoMapper;
using CrossfitDiary.Model;
using CrossfitDiary.Web.ViewModels;

namespace CrossfitDiary.Web.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName => nameof(ViewModelToDomainMappingProfile);

        public ViewModelToDomainMappingProfile()
        {
            CreateMap<MeasureTypeViewModel, MeasureType>();
            CreateMap<ExerciseMeasureTypeViewModel, ExerciseMeasureType>();
            CreateMap<ExerciseMeasureViewModel, ExerciseMeasure>();

            CreateMap<WorkoutViewModel, RoutineComplex>()
                .ForMember(x => x.ComplexType, x => x.MapFrom(y => y.WorkoutTypeViewModel))
                .ForMember(x => x.RoutineSimple, x => x.MapFrom(y => y.ExercisesToDoList))
                .ForMember(x => x.RoundCount, x => x.MapFrom(y => y.RoundsCount));

            CreateMap<ExerciseViewModel, RoutineSimple>()
                .ForMember(x => x.ExerciseId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.TimeToWork, opt => opt.ResolveUsing<TimeSpanResolver>())
                .ForMember(x => x.Count, opt => opt.ResolveUsing<CountResolver>())
                .ForMember(x => x.Distance, opt => opt.ResolveUsing<DistanceResolver>())
                .ForMember(x => x.Weight, opt => opt.ResolveUsing<WeightResolver>());

            CreateMap<ToLogWorkoutViewModel, CrossfitterWorkout>()
                .ForMember(x => x.RoutineComplexId, x => x.MapFrom(y => y.SelectedWorkoutId))
                .ForMember(x => x.Date, x => x.MapFrom(y => DateTime.Now))
                .ForMember(x => x.IsModified, x => x.MapFrom(y => !y.IsRx));
//                .ForMember(x => x.Crossfitter, x => x.MapFrom(y => System.Web.HttpContext.Current.User));
        }
    }

    public class TimeSpanResolver : IValueResolver<ExerciseViewModel, RoutineSimple, TimeSpan?>
    {
        public TimeSpan? Resolve(ExerciseViewModel source, RoutineSimple destination, TimeSpan? destMember, ResolutionContext context)
        {
            ExerciseMeasureViewModel foundTimeMeasure = source.ExerciseMeasures.SingleOrDefault(x => x.ExerciseMeasureType.MeasureType == MeasureTypeViewModel.Time);
            if (string.IsNullOrEmpty(foundTimeMeasure?.ExerciseMeasureType.MeasureValue))
                return null;
            return TimeSpan.Parse(foundTimeMeasure.ExerciseMeasureType.MeasureValue);
        }
    }
    public class CountResolver : IValueResolver<ExerciseViewModel, RoutineSimple, int?>
    {
        public int? Resolve(ExerciseViewModel source, RoutineSimple destination, int? destMember, ResolutionContext context)
        {
            ExerciseMeasureViewModel foundTimeMeasure = source.ExerciseMeasures.SingleOrDefault(x => x.ExerciseMeasureType.MeasureType == MeasureTypeViewModel.Count);
            if (string.IsNullOrEmpty(foundTimeMeasure?.ExerciseMeasureType.MeasureValue))
                return null;
            return int.Parse(foundTimeMeasure.ExerciseMeasureType.MeasureValue);
        }
    }

    public class DistanceResolver : IValueResolver<ExerciseViewModel, RoutineSimple, int?>
    {
        public int? Resolve(ExerciseViewModel source, RoutineSimple destination, int? destMember, ResolutionContext context)
        {
            ExerciseMeasureViewModel foundTimeMeasure = source.ExerciseMeasures.SingleOrDefault(x => x.ExerciseMeasureType.MeasureType == MeasureTypeViewModel.Distance);
            if (string.IsNullOrEmpty(foundTimeMeasure?.ExerciseMeasureType.MeasureValue))
                return null;
            return int.Parse(foundTimeMeasure.ExerciseMeasureType.MeasureValue);
        }
    }

    public class WeightResolver : IValueResolver<ExerciseViewModel, RoutineSimple, int?>
    {
        public int? Resolve(ExerciseViewModel source, RoutineSimple destination, int? destMember, ResolutionContext context)
        {
            ExerciseMeasureViewModel foundTimeMeasure = source.ExerciseMeasures.SingleOrDefault(x => x.ExerciseMeasureType.MeasureType == MeasureTypeViewModel.Weight);
            if (string.IsNullOrEmpty(foundTimeMeasure?.ExerciseMeasureType.MeasureValue))
                return null;
            return int.Parse(foundTimeMeasure.ExerciseMeasureType.MeasureValue);
        }
    }
}
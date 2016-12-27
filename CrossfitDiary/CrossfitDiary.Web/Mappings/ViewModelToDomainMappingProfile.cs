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
                .ForMember(x => x.RoundCount, x => x.MapFrom(y => y.RoundsCount))
                .ForMember(x => x.TimeToWork, x => x.MapFrom(y => new TimeSpan(0, int.Parse(y.TimeToWork.Split(':')[0]), int.Parse(y.TimeToWork.Split(':')[1]))))
                .ForMember(x => x.RestBetweenRounds, x => x.MapFrom(y => new TimeSpan(0, int.Parse(y.RestBetweenRounds.Split(':')[0]), int.Parse(y.RestBetweenRounds.Split(':')[1]))))
                .ForMember(x => x.RestBetweenExercises, x => x.MapFrom(y => new TimeSpan(0, int.Parse(y.RestBetweenExercises.Split(':')[0]), int.Parse(y.RestBetweenExercises.Split(':')[1]))));

            
            CreateMap<ExerciseViewModel, RoutineSimple>()
                .ForMember(x => x.ExerciseId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.Id, x => x.Ignore())
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

    public class CountResolver : IValueResolver<ExerciseViewModel, RoutineSimple, decimal?>
    {
        public decimal? Resolve(ExerciseViewModel source, RoutineSimple destination, decimal? destMember, ResolutionContext context)
        {
            ExerciseMeasureViewModel foundTimeMeasure = source.ExerciseMeasures.SingleOrDefault(x => x.ExerciseMeasureType.MeasureType == MeasureTypeViewModel.Count);
            if (string.IsNullOrEmpty(foundTimeMeasure?.ExerciseMeasureType.MeasureValue))
                return null;
            return decimal.Parse(foundTimeMeasure.ExerciseMeasureType.MeasureValue);
        }
    }

    public class DistanceResolver : IValueResolver<ExerciseViewModel, RoutineSimple, decimal?>
    {
        public decimal? Resolve(ExerciseViewModel source, RoutineSimple destination, decimal? destMember, ResolutionContext context)
        {
            ExerciseMeasureViewModel foundTimeMeasure = source.ExerciseMeasures.SingleOrDefault(x => x.ExerciseMeasureType.MeasureType == MeasureTypeViewModel.Distance);
            if (string.IsNullOrEmpty(foundTimeMeasure?.ExerciseMeasureType.MeasureValue))
                return null;
            return decimal.Parse(foundTimeMeasure.ExerciseMeasureType.MeasureValue);
        }
    }

    public class WeightResolver : IValueResolver<ExerciseViewModel, RoutineSimple, decimal?>
    {
        public decimal? Resolve(ExerciseViewModel source, RoutineSimple destination, decimal? destMember, ResolutionContext context)
        {
            ExerciseMeasureViewModel foundTimeMeasure = source.ExerciseMeasures.SingleOrDefault(x => x.ExerciseMeasureType.MeasureType == MeasureTypeViewModel.Weight);
            if (string.IsNullOrEmpty(foundTimeMeasure?.ExerciseMeasureType.MeasureValue))
                return null;
            return decimal.Parse(foundTimeMeasure.ExerciseMeasureType.MeasureValue);
        }
    }
}
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
                .ForMember(x => x.TimeToWork, opt => opt.ResolveUsing<TimeSpanResolver>());
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
}
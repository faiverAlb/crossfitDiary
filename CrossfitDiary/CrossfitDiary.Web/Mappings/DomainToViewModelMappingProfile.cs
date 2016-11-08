using AutoMapper;
using CrossfitDiary.Model;
using CrossfitDiary.Web.ViewModels;

namespace CrossfitDiary.Web.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName => nameof(DomainToViewModelMappingProfile);

        public DomainToViewModelMappingProfile()
        {
            CreateMap<Exercise, ExerciseViewModel>();
            CreateMap<MeasureType, MeasureTypeViewModel>();
            CreateMap<ExerciseMeasureType, ExerciseMeasureTypeViewModel>();
            CreateMap<ExerciseMeasure, ExerciseMeasureViewModel>();
            CreateMap<RoutineComplexType, WorkoutTypeViewModel>();
        }
    }
}
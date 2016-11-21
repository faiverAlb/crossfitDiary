using System;
using System.Collections.Generic;
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


            CreateMap<RoutineComplex, WorkoutViewModel>()
                .ForMember(x => x.WorkoutTypeViewModel, x => x.MapFrom(y => y.ComplexType))
                .ForMember(x => x.ExercisesToDoList, x => x.MapFrom(y => y.RoutineSimple))
                .ForMember(x => x.RoundsCount, x => x.MapFrom(y => y.RoundCount));

            CreateMap<RoutineSimple, ExerciseViewModel>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.ExerciseId))
                .ForMember(x => x.ExerciseMeasures, opt => opt.Ignore())
                .ForMember(x => x.Title, opt => opt.MapFrom(y => y.Exercise.Title))
                .AfterMap((simple, dest) =>
                {
                    var toMapExercises = new List<ExerciseMeasureViewModel>();
                    foreach (ExerciseMeasure exerciseMeasure in simple.Exercise.ExerciseMeasures)
                    {
                        var exerviseMeasureVm = new ExerciseMeasureViewModel() {ExerciseMeasureType = new ExerciseMeasureTypeViewModel()  };
                        switch (exerciseMeasure.ExerciseMeasureType.MeasureType)
                        {
                            case MeasureType.Distance:
                                exerviseMeasureVm.ExerciseMeasureType.MeasureType = MeasureTypeViewModel.Distance;
                                exerviseMeasureVm.ExerciseMeasureType.MeasureValue = simple.Distance?.ToString();
                                break;
                            case MeasureType.Count:
                                exerviseMeasureVm.ExerciseMeasureType.MeasureType = MeasureTypeViewModel.Count;
                                exerviseMeasureVm.ExerciseMeasureType.MeasureValue = simple.Count?.ToString();
                                break;
                            case MeasureType.Time:
                                exerviseMeasureVm.ExerciseMeasureType.MeasureType = MeasureTypeViewModel.Time;
                                exerviseMeasureVm.ExerciseMeasureType.MeasureValue = simple.TimeToWork?.ToString();
                                break;
                            case MeasureType.Weight:
                                exerviseMeasureVm.ExerciseMeasureType.MeasureType = MeasureTypeViewModel.Weight;
                                exerviseMeasureVm.ExerciseMeasureType.MeasureValue = simple.Weight?.ToString();
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        toMapExercises.Add(exerviseMeasureVm);
                    }
                    dest.ExerciseMeasures = toMapExercises;
                });
        }
    }
}
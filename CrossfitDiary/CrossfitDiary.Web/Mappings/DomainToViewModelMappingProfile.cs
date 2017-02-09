using System;
using System.Collections.Generic;
using System.Linq;
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


            CreateMap<CrossfitterWorkout, ToLogWorkoutViewModel>()
                .ForMember(x => x.SelectedWorkoutName, x => x.MapFrom(y => y.RoutineComplex.Title))
                .ForMember(x => x.SelectedWorkoutId, x => x.MapFrom(y => y.RoutineComplex.Id))
                .ForMember(x => x.CrossfitterWorkoutId, x => x.MapFrom(y => y.Id));

            CreateMap<RoutineComplex, WorkoutViewModel>()
                .ForMember(x => x.WorkoutTypeViewModel, x => x.MapFrom(y => y.ComplexType))
                .ForMember(x => x.ExercisesToDoList, x => x.MapFrom(y => y.RoutineSimple))
                .ForMember(x => x.RoundsCount, x => x.MapFrom(y => y.RoundCount))
                .ForMember(x => x.TimeToWork, x => x.MapFrom(y => y.TimeToWork.HasValue?$"{Math.Floor(y.TimeToWork.Value.TotalMinutes)}:{y.TimeToWork.Value.Seconds}" :""))
                .ForMember(x => x.RestBetweenExercises, x => x.MapFrom(y => y.RestBetweenExercises.HasValue?$"{Math.Floor(y.RestBetweenExercises.Value.TotalMinutes)}:{y.RestBetweenExercises.Value.Seconds}" :""))
                .ForMember(x => x.RestBetweenRounds, x => x.MapFrom(y => y.RestBetweenRounds.HasValue?$"{Math.Floor(y.RestBetweenRounds.Value.TotalMinutes)}:{y.RestBetweenRounds.Value.Seconds}" :""));

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
                                exerviseMeasureVm.ExerciseMeasureType.Description = simple.Exercise.ExerciseMeasures.Single(x => x.ExerciseMeasureType.MeasureType == MeasureType.Distance).ExerciseMeasureType.Description;
                                exerviseMeasureVm.ExerciseMeasureType.ShortMeasureDescription = simple.Exercise.ExerciseMeasures.Single(x => x.ExerciseMeasureType.MeasureType == MeasureType.Distance).ExerciseMeasureType.ShortMeasureDescription;
                                break;
                            case MeasureType.Count:
                                exerviseMeasureVm.ExerciseMeasureType.MeasureType = MeasureTypeViewModel.Count;
                                exerviseMeasureVm.ExerciseMeasureType.MeasureValue = simple.Count?.ToString();
                                exerviseMeasureVm.ExerciseMeasureType.Description = simple.Exercise.ExerciseMeasures.Single(x => x.ExerciseMeasureType.MeasureType == MeasureType.Count).ExerciseMeasureType.Description;
                                exerviseMeasureVm.ExerciseMeasureType.ShortMeasureDescription = simple.Exercise.ExerciseMeasures.Single(x => x.ExerciseMeasureType.MeasureType == MeasureType.Count).ExerciseMeasureType.ShortMeasureDescription;
                                break;
                            case MeasureType.Weight:
                                exerviseMeasureVm.ExerciseMeasureType.MeasureType = MeasureTypeViewModel.Weight;
                                exerviseMeasureVm.ExerciseMeasureType.MeasureValue = simple.Weight?.ToString();
                                exerviseMeasureVm.ExerciseMeasureType.Description = simple.Exercise.ExerciseMeasures.Single(x => x.ExerciseMeasureType.MeasureType == MeasureType.Weight).ExerciseMeasureType.Description;
                                exerviseMeasureVm.ExerciseMeasureType.ShortMeasureDescription = simple.Exercise.ExerciseMeasures.Single(x => x.ExerciseMeasureType.MeasureType == MeasureType.Weight).ExerciseMeasureType.ShortMeasureDescription;
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
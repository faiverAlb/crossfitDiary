using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CrossfitDiary.Model;
using CrossfitDiary.Web.Mappings.Resolvers;
using CrossfitDiary.Web.ViewModels;
using CrossfitDiary.Web.ViewModels.Pride;

namespace CrossfitDiary.Web.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName => nameof(DomainToViewModelMappingProfile);

        public DomainToViewModelMappingProfile()
        {
            CreateMap<Exercise, ExerciseViewModel>()
                .AfterMap((exercise, model) =>
                {
                    foreach (var exerciseMeasureViewModel in model.ExerciseMeasures)
                    {
                        switch (exerciseMeasureViewModel.ExerciseMeasureType.MeasureType)
                        {
                            case MeasureTypeViewModel.Weight:
                                exerciseMeasureViewModel.ExerciseMeasureType.IsRequired = exercise.ExerciseMeasures.Count <= 1;
                                break;
                        }
                    }
                });
            CreateMap<MeasureType, MeasureTypeViewModel>();
            CreateMap<ExerciseMeasureType, ExerciseMeasureTypeViewModel>();
            CreateMap<ExerciseMeasure, ExerciseMeasureViewModel>();
            CreateMap<PersonMaximum, PersonExerciseMaximumViewModel>()
                .ForMember(x => x.Date, x => x.MapFrom(y => y.Date.ToString("d")))
                .ForMember(x => x.MaximumWeightValue, x => x.MapFrom(y => y.MaximumWeight))
                .ForMember(x => x.MaximumWeight, x => x.MapFrom(y => y.MaximumWeight.HasValue == false? "-": $"{y.MaximumWeight.ToCustomString()} kg"));


            CreateMap<CrossfitterWorkout, ToLogWorkoutViewModel>()
                .ForMember(x => x.SelectedWorkoutName, x => x.MapFrom(y => y.RoutineComplex.Title))
                .ForMember(x => x.SelectedWorkoutId, x => x.MapFrom(y => y.RoutineComplex.Id))
                .ForMember(x => x.CrossfitterWorkoutId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.CanBeRemovedByCurrentUser, x => x.ResolveUsing<CanBeRemovedResolver>())
                .ForMember(x => x.WorkouterName, x => x.MapFrom(y => y.Crossfitter.FullName))
                .ForMember(x => x.WorkoutViewModel, x => x.MapFrom(y => y.RoutineComplex))
                .ForMember(x => x.WorkouterId, x => x.MapFrom(y => y.Crossfitter.Id))
                .ForMember(x => x.TimePassed, x => x.ResolveUsing<TimePassedResolver>());

            CreateMap<RoutineComplex, WorkoutViewModel>()
                .ForMember(x => x.WorkoutType, x => x.MapFrom(y => y.ComplexType))
                .ForMember(x => x.Children, x => x.MapFrom(y => y.OrderedChildren))
                .ForMember(x => x.ExercisesToDoList, x => x.MapFrom(y => y.RoutineSimple))
                .ForMember(x => x.RoundsCount, x => x.MapFrom(y => y.RoundCount))
                .ForMember(x => x.TimeToWork,
                    x => x.MapFrom(y =>
                        y.TimeToWork.HasValue
                            ? $"{Math.Floor(y.TimeToWork.Value.TotalMinutes):00}:{y.TimeToWork.Value.Seconds:00}"
                            : string.Empty))
                .ForMember(x => x.TimeCap,
                    x => x.MapFrom(y =>
                        y.TimeCap.HasValue
                            ? $"{Math.Floor(y.TimeCap.Value.TotalMinutes):00}:{y.TimeCap.Value.Seconds:00}"
                            : string.Empty))
                .ForMember(x => x.RestBetweenExercises,
                    x => x.MapFrom(y =>
                        y.RestBetweenExercises.HasValue
                            ? $"{Math.Floor(y.RestBetweenExercises.Value.TotalMinutes)}:{y.RestBetweenExercises.Value.Seconds}"
                            : string.Empty))
                .ForMember(x => x.RestBetweenRounds, x => x.MapFrom(y => y.RestBetweenRounds.HasValue?$"{Math.Floor(y.RestBetweenRounds.Value.TotalMinutes)}:{y.RestBetweenRounds.Value.Seconds}" :string.Empty));

            CreateMap<RoutineSimple, ExerciseViewModel>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.ExerciseId))
                .ForMember(x => x.ExerciseMeasures, opt => opt.Ignore())
                .ForMember(x => x.Title, opt => opt.MapFrom(y => y.Exercise.Title))
                .ForMember(x => x.IsDoUnbroken, opt => opt.MapFrom(y => y.IsDoUnbroken))
                .AfterMap((simple, dest) =>
                {
                    var toMapExercises = new List<ExerciseMeasureViewModel>();
                    foreach (ExerciseMeasure exerciseMeasure in simple.Exercise.ExerciseMeasures)
                    {
                        var exerviseMeasureVm = new ExerciseMeasureViewModel() {ExerciseMeasureType = new ExerciseMeasureTypeViewModel()};
                        switch (exerciseMeasure.ExerciseMeasureType.MeasureType)
                        {
                            case MeasureType.Distance:
                                exerviseMeasureVm.ExerciseMeasureType.MeasureType = MeasureTypeViewModel.Distance;
                                exerviseMeasureVm.ExerciseMeasureType.MeasureValue = simple.Distance.ToCustomString();
                                exerviseMeasureVm.ExerciseMeasureType.Description = simple.Exercise.ExerciseMeasures.Single(x => x.ExerciseMeasureType.MeasureType == MeasureType.Distance).ExerciseMeasureType.Description;
                                exerviseMeasureVm.ExerciseMeasureType.ShortMeasureDescription = simple.Exercise.ExerciseMeasures.Single(x => x.ExerciseMeasureType.MeasureType == MeasureType.Distance).ExerciseMeasureType.ShortMeasureDescription;
                                break;
                            case MeasureType.Count:
                                exerviseMeasureVm.ExerciseMeasureType.MeasureType = MeasureTypeViewModel.Count;
                                exerviseMeasureVm.ExerciseMeasureType.MeasureValue = $"{simple.Count:0}";
                                exerviseMeasureVm.ExerciseMeasureType.Description = simple.Exercise.ExerciseMeasures.Single(x => x.ExerciseMeasureType.MeasureType == MeasureType.Count).ExerciseMeasureType.Description;
                                exerviseMeasureVm.ExerciseMeasureType.ShortMeasureDescription = simple.Exercise.ExerciseMeasures.Single(x => x.ExerciseMeasureType.MeasureType == MeasureType.Count).ExerciseMeasureType.ShortMeasureDescription;
                                break;
                            case MeasureType.Weight:
                                exerviseMeasureVm.ExerciseMeasureType.MeasureType = MeasureTypeViewModel.Weight;
                                exerviseMeasureVm.ExerciseMeasureType.MeasureValue = simple.Weight.ToCustomString();
                                exerviseMeasureVm.ExerciseMeasureType.Description = simple.Exercise.ExerciseMeasures.Single(x => x.ExerciseMeasureType.MeasureType == MeasureType.Weight).ExerciseMeasureType.Description;
                                exerviseMeasureVm.ExerciseMeasureType.ShortMeasureDescription = simple.Exercise.ExerciseMeasures.Single(x => x.ExerciseMeasureType.MeasureType == MeasureType.Weight).ExerciseMeasureType.ShortMeasureDescription;
                                exerviseMeasureVm.ExerciseMeasureType.IsRequired = simple.Exercise.ExerciseMeasures.Count <= 1;
                                break;
                            case MeasureType.Calories:
                                exerviseMeasureVm.ExerciseMeasureType.MeasureType = MeasureTypeViewModel.Calories;
                                exerviseMeasureVm.ExerciseMeasureType.MeasureValue = $"{simple.Calories:0}";
                                exerviseMeasureVm.ExerciseMeasureType.Description = simple.Exercise.ExerciseMeasures.Single(x => x.ExerciseMeasureType.MeasureType == MeasureType.Calories).ExerciseMeasureType.Description;
                                exerviseMeasureVm.ExerciseMeasureType.ShortMeasureDescription = simple.Exercise.ExerciseMeasures.Single(x => x.ExerciseMeasureType.MeasureType == MeasureType.Calories).ExerciseMeasureType.ShortMeasureDescription;
                                break;
                            case MeasureType.Height:
                                exerviseMeasureVm.ExerciseMeasureType.MeasureType = MeasureTypeViewModel.Height;
                                exerviseMeasureVm.ExerciseMeasureType.MeasureValue = $"{simple.Centimeters:0}";
                                exerviseMeasureVm.ExerciseMeasureType.Description = simple.Exercise.ExerciseMeasures.Single(x => x.ExerciseMeasureType.MeasureType == MeasureType.Height).ExerciseMeasureType.Description;
                                exerviseMeasureVm.ExerciseMeasureType.ShortMeasureDescription = simple.Exercise.ExerciseMeasures.Single(x => x.ExerciseMeasureType.MeasureType == MeasureType.Height).ExerciseMeasureType.ShortMeasureDescription;
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
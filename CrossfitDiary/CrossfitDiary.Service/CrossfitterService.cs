using System;
using System.Collections.Generic;
using System.Linq;
using CrossfitDiary.DAL.EF.Infrastructure;
using CrossfitDiary.DAL.EF.Repositories;
using CrossfitDiary.Model;
using CrossfitDiary.Service.Interfaces;

namespace CrossfitDiary.Service
{
    public class CrossfitterService : ICrossfitterService
    {
        private readonly IRoutineComplexRepository _routineComplexRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly ICrossfitterWorkoutRepository _crossfitterWorkoutRepository;

        public CrossfitterService(IRoutineComplexRepository routineComplexRepository, IUnitOfWork unitOfWork, IExerciseRepository exerciseRepository, ICrossfitterWorkoutRepository crossfitterWorkoutRepository)
        {
            _routineComplexRepository = routineComplexRepository;
            _unitOfWork = unitOfWork;
            _exerciseRepository = exerciseRepository;
            _crossfitterWorkoutRepository = crossfitterWorkoutRepository;
        }

        public void CreateWorkout(RoutineComplex routineComplexToSave)
        {
            SetRoutineComplexTitle(routineComplexToSave);

            _routineComplexRepository.Add(routineComplexToSave);
            _unitOfWork.Commit();
        }

        public void LogWorkout(CrossfitterWorkout workoutToLog)
        {
            _crossfitterWorkoutRepository.AddOrUpdate(workoutToLog);
            _unitOfWork.Commit();
        }

        private void SetRoutineComplexTitle(RoutineComplex routineComplexToSave)
        {
            if (!string.IsNullOrEmpty(routineComplexToSave.Title))
                return;

            List<string> exerciseNames = new List<string>();
            foreach (var routineSimple in routineComplexToSave.RoutineSimple)
            {
                Exercise exercise = _exerciseRepository.GetById(routineSimple.ExerciseId);
                exerciseNames.Add(exercise.Abbreviation);
            }
            routineComplexToSave.Title = $"{routineComplexToSave.ComplexType}: {string.Join(", ", exerciseNames)}";
        }

        public List<CrossfitterWorkout> GetCrossfitterWorkouts(string userId, DateTime fromDate, DateTime dueDate)
        {
            var crossfitterWorkouts = _crossfitterWorkoutRepository.GetMany(x => x.Crossfitter.Id == userId && (fromDate < x.Date && x.Date <= dueDate)).ToList();
            foreach (var workout in crossfitterWorkouts)
            {
                if (workout.Distance.HasValue)
                    workout.MeasureDisplayName = $"{workout.Distance.Value}m";
                if (workout.RoundsFinished.HasValue)
                { 
                    workout.MeasureDisplayName = $"{workout.RoundsFinished.Value} rnds";
                    if (workout.PartialRepsFinished.HasValue)
                    {
                        workout.MeasureDisplayName = $"{workout.MeasureDisplayName} + {workout.PartialRepsFinished.Value}";
                    }
                }

                if (workout.TimePassed.HasValue )
                {
                    workout.MeasureDisplayName = $"{workout.TimePassed.Value.Minutes}min";
                    if (workout.TimePassed.Value.Seconds>0)
                    {
                        workout.MeasureDisplayName = $"{workout.MeasureDisplayName}+{workout.TimePassed.Value.Seconds}sec";
                    }

                }

            }
            return crossfitterWorkouts;
        }
    }
}
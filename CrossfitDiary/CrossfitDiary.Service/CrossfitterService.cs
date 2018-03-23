using System;
using System.Collections.Generic;
using System.Linq;
using CrossfitDiary.DAL.EF.Infrastructure;
using CrossfitDiary.DAL.EF.Repositories;
using CrossfitDiary.Model;

namespace CrossfitDiary.Service
{
    public class CrossfitterService
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

        public int CreateWorkout(RoutineComplex routineComplexToSave)
        {
            if (IsAlreadyExist(routineComplexToSave, out int workoutId))
            {
                return workoutId;
            }

            SetRoutineComplexTitle(routineComplexToSave);
            _routineComplexRepository.Add(routineComplexToSave);
            _unitOfWork.Commit();
            return routineComplexToSave.Id;
        }

        private bool IsAlreadyExist(RoutineComplex routineComplexToSave, out int workoutId)
        {
            CrossfitterWorkout workout  =  _crossfitterWorkoutRepository.FirstOrDefault(x => x.Crossfitter == routineComplexToSave.CreatedBy);
            workoutId = workout?.Id ?? 0;
            return workout == null;
        }

        public void LogWorkout(CrossfitterWorkout workoutToLog)
        {
            _crossfitterWorkoutRepository.AddOrUpdate(workoutToLog);
            _unitOfWork.Commit();
        }

        public void CreateAndLogNewWorkout(RoutineComplex newWorkoutRoutine, CrossfitterWorkout logWorkoutModel)
        {
            int newWorkoutId = CreateWorkout(newWorkoutRoutine);
            logWorkoutModel.RoutineComplexId = newWorkoutId;
            LogWorkout(logWorkoutModel);
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


        /// <summary>
        /// The get person maximum for exercise.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="exerciseId">
        ///     The exercise id.
        /// </param>
        /// <param name="getUserId"></param>
        /// <returns>
        /// The <see cref="PersonMaximum"/>.
        /// </returns>
        public PersonMaximum GetPersonMaximumForExercise(string userId, int exerciseId)
        {
            IEnumerable<CrossfitterWorkout> workoutsWithExericesOnly = _crossfitterWorkoutRepository
                        .GetMany(x => x.Crossfitter.Id == userId
                                      && x.RoutineComplex.RoutineSimple.Any(y => y.ExerciseId == exerciseId));

            PersonMaximum maximum = (from crossfitterWorkout in workoutsWithExericesOnly
                select new PersonMaximum
                {
                    Date = crossfitterWorkout.Date,
                    PersonName = crossfitterWorkout.Crossfitter.FullName,
                    PersonId = crossfitterWorkout.Crossfitter.Id,
                    WorkoutTitle = crossfitterWorkout.RoutineComplex.Title,
                    MaximumWeight = (
                        from exercise in crossfitterWorkout.RoutineComplex.RoutineSimple
                        select exercise.Weight ?? 0
                    ).Max()
                }).OrderByDescending(x => x.MaximumWeight).FirstOrDefault();
            return maximum;

        }


        public List<PersonMaximum> GetAllPersonMaximumForExercise(int exerciseId, string currentUserId)
        {
            IEnumerable<CrossfitterWorkout> workoutsWithExericesOnly = _crossfitterWorkoutRepository.GetMany(x => x.RoutineComplex.RoutineSimple.Any(y => y.ExerciseId == exerciseId));
            var groupedByCrossfitter = workoutsWithExericesOnly.GroupBy(x => x.Crossfitter.Id);
            var resultMaximums = new List<PersonMaximum>();
            foreach (var group in groupedByCrossfitter)
            {
                resultMaximums.Add(GetPersonMaximumForExercise(group.Key, exerciseId));
            }

            resultMaximums = resultMaximums.OrderByDescending(x => x.MaximumWeight).ToList();
            for (int i = 0; i < resultMaximums.Count; i++)
            {
                resultMaximums[i].PositionBetweenOthers = i + 1;
                resultMaximums[i].IsItMe = resultMaximums[i].PersonId == currentUserId;
            }

            return resultMaximums;
        }


        /// <summary>
        /// Returns all crossfitters workouts.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<CrossfitterWorkout> GetAllCrossfittersWorkouts()
        {
            List<CrossfitterWorkout> crossfitterWorkouts = _crossfitterWorkoutRepository.GetAll().ToList();
            foreach (var workout in crossfitterWorkouts)
            {
                PrepareWorkout(workout);
            }

            return crossfitterWorkouts.OrderByDescending(x => x.Date).ToList();
        }

        private void PrepareWorkout(CrossfitterWorkout workout)
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

            if (workout.TimePassed.HasValue)
            {
                workout.MeasureDisplayName = $"{workout.TimePassed.Value.Minutes}min";
                if (workout.TimePassed.Value.Seconds > 0)
                {
                    workout.MeasureDisplayName = $"{workout.MeasureDisplayName}+{workout.TimePassed.Value.Seconds}sec";
                }

            }
        }

        public void RemoveWorkout(int crossfitterWorkoutId)
        {
            _crossfitterWorkoutRepository.Delete(x => x.Id == crossfitterWorkoutId);
            _unitOfWork.Commit();
        }

    }
}
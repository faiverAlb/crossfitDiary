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
            int workoutId = FindDefaultOrExistingWorkout(routineComplexToSave);
            if (workoutId != 0)
            {
                return workoutId;
            }

            SetRoutineComplexTitle(routineComplexToSave);
            _routineComplexRepository.Add(routineComplexToSave);
            _unitOfWork.Commit();
            return routineComplexToSave.Id;
        }

        /// <summary>
        /// Find existing workout by workout structure
        /// </summary>
        /// <param name="routineComplexToSave">
        /// The routine complex to save.
        /// </param>
        /// <param name="workoutId">
        /// The workout id if found
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual int FindDefaultOrExistingWorkout(RoutineComplex routineComplexToSave)
        {
            List<RoutineComplex> workoutsToCheck = _routineComplexRepository.GetAll().ToList();


            foreach (RoutineComplex existingRoutineComplex in workoutsToCheck)
            {
                if (routineComplexToSave.ComplexType != existingRoutineComplex.ComplexType)
                {
                    continue;
                }

                if (routineComplexToSave.RoundCount != existingRoutineComplex.RoundCount)
                {
                    continue;
                }

                if (routineComplexToSave.TimeToWork != existingRoutineComplex.TimeToWork)
                {
                    continue;
                }

                if (routineComplexToSave.TimeCap != existingRoutineComplex.TimeCap)
                {
                    continue;
                }

                if (routineComplexToSave.RoutineSimple.Count != existingRoutineComplex.RoutineSimple.Count)
                {
                    continue;
                }

                bool isExercisesMatch = true;

                for (int i = 0; i < routineComplexToSave.RoutineSimple.Count; i++)
                {
                    RoutineSimple routineSimpleToSave = routineComplexToSave.RoutineSimple.ToList()[i];
                    RoutineSimple existingSimpleRoutine = existingRoutineComplex.RoutineSimple.ToList()[i];
                    if (routineSimpleToSave.ExerciseId != existingSimpleRoutine.ExerciseId
                        || routineSimpleToSave.Count != existingSimpleRoutine.Count 
                        || routineSimpleToSave.Distance != existingSimpleRoutine.Distance
                        || routineSimpleToSave.Weight != existingSimpleRoutine.Weight
                        || routineSimpleToSave.Calories != existingSimpleRoutine.Calories
                        || routineSimpleToSave.Centimeters != existingSimpleRoutine.Centimeters)
                    {
                        isExercisesMatch = false;
                        break;
                    }
                }

                if (isExercisesMatch == false)
                {
                    continue;
                }

                return existingRoutineComplex.Id;
            }

            return 0;
        }

        /// <summary>
        /// Log workout: delete first old if <param name="isEditMode">equals true</param> otherwise just log
        /// </summary>
        /// <param name="workoutToLog">
        /// The workout to log.
        /// </param>
        /// <param name="isEditMode">
        /// The is edit mode.
        /// </param>
        public void LogWorkout(CrossfitterWorkout workoutToLog, bool isEditMode)
        {
            if (isEditMode)
            {
                _crossfitterWorkoutRepository.Delete(x => x.Id == workoutToLog.Id);
                _unitOfWork.Commit();
            }
            _crossfitterWorkoutRepository.AddOrUpdate(workoutToLog);
            _unitOfWork.Commit();
        }

        public void CreateAndLogNewWorkout(RoutineComplex newWorkoutRoutine, CrossfitterWorkout logWorkoutModel, bool isEditMode)
        {
            int newWorkoutId = CreateWorkout(newWorkoutRoutine);
            logWorkoutModel.RoutineComplexId = newWorkoutId;
            LogWorkout(logWorkoutModel, isEditMode);
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
            string exerciseAbbreviation = _exerciseRepository.GetById(exerciseId).Abbreviation;

            PersonMaximum maximum = (from crossfitterWorkout in workoutsWithExericesOnly
                select new PersonMaximum
                {
                    Date = crossfitterWorkout.Date,
                    PersonName = crossfitterWorkout.Crossfitter.FullName,
                    PersonId = crossfitterWorkout.Crossfitter.Id,
                    WorkoutTitle = crossfitterWorkout.RoutineComplex.Title,
                    WorkoutId = crossfitterWorkout.RoutineComplex.Id,
                    ExerciseDisplayName = exerciseAbbreviation,
                    ExerciseId = exerciseId,
                    MaximumWeight = (
                        from exercise in crossfitterWorkout.RoutineComplex.RoutineSimple
                        where exercise.ExerciseId == exerciseId
                        select exercise.Weight
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
        public List<CrossfitterWorkout> GetAllCrossfittersWorkouts(string userId, int? exerciseId)
        {
            List<CrossfitterWorkout> crossfitterWorkouts = string.IsNullOrEmpty(userId)?_crossfitterWorkoutRepository.GetAll().ToList() : _crossfitterWorkoutRepository.GetMany(x => x.Crossfitter.Id == userId).ToList();
            crossfitterWorkouts = FilterWorkoutsOnSelectedExercise(crossfitterWorkouts, exerciseId);
            return crossfitterWorkouts.OrderByDescending(x => x.Date).ThenByDescending(x => x.CreatedUtc).ToList();
        }

        /// <summary>
        /// Filters workouts having the exercise if it not null
        /// </summary>
        /// <param name="crossfitterWorkouts"></param>
        /// <param name="exerciseId"></param>
        private List<CrossfitterWorkout> FilterWorkoutsOnSelectedExercise(List<CrossfitterWorkout> crossfitterWorkouts, int? exerciseId)
        {
            if (exerciseId.HasValue == false)
            {
                return crossfitterWorkouts;
            }

            return crossfitterWorkouts.Where(x => x.RoutineComplex.RoutineSimple.Any(y => y.ExerciseId == exerciseId)).ToList();
        }


        /// <summary>
        /// The get crossfitter workout.
        /// </summary>
        /// <param name="crossfitterWorkoutId"></param>
        /// <returns>
        /// The <see cref="CrossfitterWorkout"/>.
        /// </returns>
        public CrossfitterWorkout GetCrossfitterWorkout(int crossfitterWorkoutId)
        {
            CrossfitterWorkout crossfitterWorkout = _crossfitterWorkoutRepository.Single(x => x.Id == crossfitterWorkoutId);
            return crossfitterWorkout;
        }

        private void SetRoutineComplexTitle(RoutineComplex routineComplexToSave)
        {
            if (!string.IsNullOrEmpty(routineComplexToSave.Title))
            {
                return;
            }

            List<string> exerciseNames = new List<string>();
            foreach (var routineSimple in routineComplexToSave.RoutineSimple)
            {
                Exercise exercise = _exerciseRepository.GetById(routineSimple.ExerciseId);
                exerciseNames.Add(exercise.Abbreviation);
            }
            routineComplexToSave.Title = $"{routineComplexToSave.ComplexType}: {string.Join(", ", exerciseNames)}";
        }


        public void RemoveWorkout(int crossfitterWorkoutId, ApplicationUser user)
        {
            _crossfitterWorkoutRepository.Delete(x => x.Id == crossfitterWorkoutId && x.Crossfitter.Id == user.Id);
            _unitOfWork.Commit();
        }

        public List<PersonMaximum> GetPersonMaximumForMainExercises(string userId, int? exerciseId)
        {
            var exercisesListTitle = new List<string> { "deadlift", "back squat", "bench press", "shoulder press (strict)", "snatch", "power snatch", "clean", "power clean" };
            var resultMaximums = new List<PersonMaximum>();

            var exercisesList = new List<Exercise>();
            foreach (string exerciseTitle in exercisesListTitle)
            {
                Exercise exercise = _exerciseRepository.FirstOrDefault(x => x.Title.ToLower() == exerciseTitle.ToLower());
                exercisesList.Add(exercise);
            }

            if (exerciseId.HasValue)
            {
                exercisesList = exercisesList.Where(x => x.Id == exerciseId).ToList();
            }

            foreach (Exercise exercise in exercisesList)
            {
                PersonMaximum personMaximumForExercise = GetPersonMaximumForExercise(userId, exercise.Id);
                if (personMaximumForExercise == null)
                {
                    continue;
                }

                personMaximumForExercise.ExerciseDisplayName = exercise.Abbreviation;
                resultMaximums.Add(personMaximumForExercise);
            }

            return resultMaximums;
        }
    }
}
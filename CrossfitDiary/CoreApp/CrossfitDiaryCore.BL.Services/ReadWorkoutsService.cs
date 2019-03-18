﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossfitDiaryCore.BL.Services.DapperStuff;
using CrossfitDiaryCore.BL.Services.WorkoutMatchers;
using CrossfitDiaryCore.DAL.EF;
using CrossfitDiaryCore.DAL.EF.Exercises;
using CrossfitDiaryCore.Model;
using CrossfitDiaryCore.Model.TempModels;
using Microsoft.EntityFrameworkCore;

namespace CrossfitDiaryCore.BL.Services
{
    public class ReadWorkoutsService
    {
        private readonly WorkouterContext _context;
        private readonly WorkoutsMatchDispatcher _workoutsMatchDispatcher;
        private readonly DapperRepository _dapperRepository;

        public ReadWorkoutsService(WorkouterContext  context, WorkoutsMatchDispatcher workoutsMatchDispatcher, DapperRepository dapperRepository)
        {
            _context = context;
            _workoutsMatchDispatcher = workoutsMatchDispatcher;
            _dapperRepository = dapperRepository;
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

            List<RoutineComplex> workoutsToCheck = _context.ComplexRoutines.Include(x => x.RoutineSimple).ThenInclude(x => x.Exercise).ThenInclude(x => x.ExerciseMeasures).ToList();
            foreach (RoutineComplex existingRoutineComplex in workoutsToCheck)
            {
                if (_workoutsMatchDispatcher.IsWorkoutsMatch(existingRoutineComplex, routineComplexToSave) == false)
                {
                    continue;
                }
                return existingRoutineComplex.Id;
            }

            return 0;
        }



        /// <summary>
        /// The get person maximum for exercise.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="exerciseId">
        ///     The exercise id.
        /// </param>
        /// <returns>
        /// The <see cref="PersonMaximum"/>.
        /// </returns>
        private PersonMaximum GetPersonMaximumForExercise(string userId, int exerciseId)
        {

            List<CrossfitterWorkout> workoutsWithExericesOnly = _context.CrossfitterWorkouts.Where(x => x.Crossfitter.Id == userId
                                      && (x.RoutineComplex.RoutineSimple.Any(y => y.ExerciseId == exerciseId)
                                      || x.RoutineComplex.Children.Any(child => child.RoutineSimple.Any(chZ => chZ.ExerciseId == exerciseId)))
                                      && x.RepsToFinishOnCapTime.HasValue == false).Include(x => x.RoutineComplex).ThenInclude(x => x.RoutineSimple).ThenInclude(x => x.Exercise).ThenInclude(x => x.ExerciseMeasures)

                        .ToList();

            
            string exerciseAbbreviation = _context.Exercises.Single(x => x.Id == exerciseId).Abbreviation;

            var routineDictionary = new Dictionary<int, List<RoutineSimple>>();
            foreach (CrossfitterWorkout workout in workoutsWithExericesOnly)
            {
                if (routineDictionary.ContainsKey(workout.Id))
                {
                    routineDictionary[workout.Id].AddRange(workout.RoutineComplex.RoutineSimple);
                }
                else
                {
                    routineDictionary[workout.Id] = new List<RoutineSimple>((workout.RoutineComplex.RoutineSimple));
                }
                routineDictionary[workout.Id].AddRange(workout.RoutineComplex.Children.SelectMany(x => x.RoutineSimple));

            }

            PersonMaximum maximum = (from crossfitterWorkout in workoutsWithExericesOnly
                select new PersonMaximum
                {
                    Date = crossfitterWorkout.Date,
                    PersonName = crossfitterWorkout.Crossfitter.FullName,
                    PersonId = crossfitterWorkout.Crossfitter.Id,
                    WorkoutTitle = crossfitterWorkout.RoutineComplex.Title,
                    CrossfitWorkoutId = crossfitterWorkout.Id,
                    ExerciseDisplayName = exerciseAbbreviation,
                    ExerciseId = exerciseId,
                    MaximumWeight = (
                        from exercise in routineDictionary.Where(x => x.Key == crossfitterWorkout.Id).SelectMany(x => x.Value)
                        where exercise.ExerciseId == exerciseId
                        select exercise.Weight
                    ).Max()
                }).OrderByDescending(x => x.MaximumWeight).ThenBy(x => x.Date).FirstOrDefault();

            if (maximum?.MaximumWeight != null)
            {
                PersonMaximum secondMaximum = (from crossfitterWorkout in workoutsWithExericesOnly
                    select new PersonMaximum
                    {
                        MaximumWeight = (
                            from exercise in routineDictionary.Where(x => x.Key == crossfitterWorkout.Id).SelectMany(x => x.Value)
                            where exercise.ExerciseId == exerciseId && exercise.Weight != maximum.MaximumWeight
                            select exercise.Weight
                        ).Max()
                    }).OrderByDescending(x => x.MaximumWeight).ThenBy(x => x.Date).FirstOrDefault();

                maximum.PreviousMaximumWeight = secondMaximum?.MaximumWeight;
            }
            return maximum;

        }
 
        /// <summary>
        ///     Returns all crossfitters workouts.
        /// </summary>
        public List<CrossfitterWorkout> GetAllCrossfittersWorkoutsAsync(string userId, int page, int pageSize)
        {
            List<int> ids = new List<int>();
            var showOnlyUserWods = _context.Users.Single(x => x.Id == userId).ShowOnlyUserWods;
            DapperResults crossfitterCombinedResults;
            if (showOnlyUserWods == false)
            {
                ids = _dapperRepository.GetIds(((page - 1) * pageSize), pageSize);
                crossfitterCombinedResults = _dapperRepository.GetCrossfitterResultsByResultIds(ids);
            }
            else
            {
                crossfitterCombinedResults = _dapperRepository.GetCrossfitterResultsByUserId(userId);
            }


            IEnumerable<ExerciseMeasure> allExerciseMeasures = _dapperRepository.GetAllExerciseMeasures();
            IEnumerable<CrossfitterWorkout> crossfitterWorkouts = crossfitterCombinedResults.CrossfitterWorkouts;

            IEnumerable<RoutineComplex> childRoutines = crossfitterCombinedResults.ChildRoutines;
            IEnumerable<RoutineSimple> simpleRoutingForChild = crossfitterCombinedResults.ChildRoutineSimples;
            IEnumerable<RoutineSimple> routineSimples = crossfitterCombinedResults.RoutineSimples;

            foreach (RoutineComplex childRoutine in childRoutines)
            {
                childRoutine.RoutineSimple = simpleRoutingForChild.Where(x => x.RoutineComplexId == childRoutine.Id).ToList();
                foreach (var routine in childRoutine.RoutineSimple)
                {
                    routine.Exercise.ExerciseMeasures = allExerciseMeasures.Where(x => x.ExerciseId == routine.ExerciseId).ToList();
                }
            }

            foreach (CrossfitterWorkout workout in crossfitterWorkouts)
            {
                workout.RoutineComplex.RoutineSimple = routineSimples.Where(x => x.RoutineComplexId == workout.RoutineComplexId).ToList();
                foreach (var routine in workout.RoutineComplex.RoutineSimple)
                {
                    routine.Exercise.ExerciseMeasures = allExerciseMeasures.Where(x => x.ExerciseId == routine.ExerciseId).ToList();
                }
                workout.RoutineComplex.Children = childRoutines.Where(x => x.ParentId == workout.RoutineComplexId).ToList();
            }

            // Commented to improve performance
            //UpdateWorkoutsWithRecords(crossfitterWorkouts);

            List<CrossfitterWorkout> allCrossfittersWorkouts = crossfitterWorkouts.OrderByDescending(x => x.Date).ThenByDescending(x => x.CreatedUtc).ToList();//.Skip(((page - 1) * pageSize)).Take(pageSize).ToList();
            foreach (CrossfitterWorkout allCrossfittersWorkout in allCrossfittersWorkouts)
            {
                allCrossfittersWorkout.RoutineComplex.Children = allCrossfittersWorkout.RoutineComplex.Children.OrderBy(x => x.Position).ToList();
            }
            return allCrossfittersWorkouts;
        }


        /// <summary>
        ///     Find exercise maximums and mark crossfitter workout as having new maximum and exercise as new max
        /// </summary>
        /// <param name="crossfitterWorkouts"></param>
        private void UpdateWorkoutsWithRecords(IEnumerable<CrossfitterWorkout> crossfitterWorkouts)
        {
            IEnumerable<IGrouping<ApplicationUser, CrossfitterWorkout>> groupedByuser = crossfitterWorkouts.GroupBy(x => x.Crossfitter);

            foreach (IGrouping<ApplicationUser, CrossfitterWorkout> personWorkouts in groupedByuser)
            {
                List<Exercise> allDistinctExercisesFromWorkouts = personWorkouts
                                .SelectMany(x => x.RoutineComplex.RoutineSimple.Select(y => y.Exercise))
                                .Union(personWorkouts.SelectMany(x => x.RoutineComplex.Children.SelectMany(z => z.RoutineSimple)).Select(x => x.Exercise))
                                .Distinct()
                                .ToList();
                foreach (Exercise exercise in allDistinctExercisesFromWorkouts)
                {
                    var user = personWorkouts.Key;
                    PersonMaximum personMaximum =  GetPersonMaximumForExercise(user.Id, exercise.Id);
                    MarkWorkoutWithWeightRecord(personMaximum, crossfitterWorkouts);
                }
            }
        }

        /// <summary>
        ///     Implement inner logic for marking crossfit workout with record
        /// </summary>
        /// <param name="personMaximum"></param>
        /// <param name="crossfitterWorkouts"></param>
        private void MarkWorkoutWithWeightRecord(PersonMaximum personMaximum, IEnumerable<CrossfitterWorkout> crossfitterWorkouts)
        {
            if (personMaximum == null || personMaximum.MaximumWeight == null || personMaximum.MaximumWeight == 0)
            {
                return;
            }

            CrossfitterWorkout workoutToAddMaximum = crossfitterWorkouts.SingleOrDefault(x => x.Id == personMaximum.CrossfitWorkoutId);
            if (workoutToAddMaximum == null)
            {
                return;
            }

            if (workoutToAddMaximum.RoutineComplex.Children.Any())
            {
                foreach (RoutineComplex routineComplexChild in workoutToAddMaximum.RoutineComplex.Children.Where(z => z.RoutineSimple.Any(x => x.ExerciseId == personMaximum.ExerciseId && x.Weight == personMaximum.MaximumWeight)))
                {
                    RoutineSimple routineSimple = routineComplexChild.RoutineSimple.First(x => x.ExerciseId == personMaximum.ExerciseId && x.Weight == personMaximum.MaximumWeight);
                    routineSimple.IsNewWeightMaximum = true;
                    routineSimple.AddedToMaxWeight = personMaximum.PreviousMaximumWeight.HasValue?personMaximum.MaximumWeight - personMaximum.PreviousMaximumWeight:null;
                }
            }
            else
            {
                RoutineSimple routineSimple = workoutToAddMaximum.RoutineComplex.RoutineSimple.First(x => x.ExerciseId == personMaximum.ExerciseId && x.Weight == personMaximum.MaximumWeight);
                routineSimple.IsNewWeightMaximum = true;
                routineSimple.AddedToMaxWeight = personMaximum.PreviousMaximumWeight.HasValue?personMaximum.MaximumWeight - personMaximum.PreviousMaximumWeight:null;
            }
        }



        /// <summary>
        /// The get crossfitter workout.
        /// </summary>
        /// <param name="crossfitterWorkoutId"></param>
        /// <returns>
        /// The <see cref="CrossfitterWorkout"/>.
        /// </returns>
        public CrossfitterWorkout GetCrossfitterWorkout(string userId, int crossfitterWorkoutId)
        {
            CrossfitterWorkout crossfitterWorkout =
                _context.CrossfitterWorkouts
                    .Include(x => x.RoutineComplex).ThenInclude(x => x.Children).ThenInclude(x => x.RoutineSimple).ThenInclude(x => x.Exercise).ThenInclude(x => x.ExerciseMeasures)
                    .Include(x => x.RoutineComplex).ThenInclude(x => x.RoutineSimple).ThenInclude(x => x.Exercise).ThenInclude(x => x.ExerciseMeasures)
                    .Include(x => x.Crossfitter)
                    .SingleOrDefault(x => x.Crossfitter.Id == userId && x.Id == crossfitterWorkoutId);

            return crossfitterWorkout;
        }

        public RoutineComplex GetWorkout(int workoutId)
        {
            return _context.ComplexRoutines
                .Include(x => x.RoutineSimple)
                .ThenInclude(x => x.Exercise)
                .ThenInclude(x => x.ExerciseMeasures)
                .Include(x => x.Children)
                .ThenInclude(x => x.RoutineSimple)
                .ThenInclude(x => x.Exercise)
                .ThenInclude(x => x.ExerciseMeasures)
                .SingleOrDefault(x => x.Id == workoutId);
        }

        public List<RoutineComplex> GetPlannedWorkouts(DateTime today)
        {
            return _context.ComplexRoutines.Where(x => x.PlanDate.HasValue && x.PlanDate.Value.Date == today.Date)
                .Include(x => x.RoutineSimple)
                .ThenInclude(x => x.Exercise)
                .ThenInclude(x => x.ExerciseMeasures)
                .Include(x => x.Children)
                .ThenInclude(x => x.RoutineSimple)
                .ThenInclude(x => x.Exercise)
                .ThenInclude(x => x.ExerciseMeasures).ToList();
        }

        public List<TempPersonMaximum> GetPersonMaxumums(string userId)
        {
            return _dapperRepository.GetPersonMaxumums(userId).ToList();
        }
    }
}
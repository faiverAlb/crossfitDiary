using System;
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


            List<ExerciseMeasure> allExerciseMeasures = _dapperRepository.GetAllExerciseMeasures().ToList();
            List<CrossfitterWorkout> crossfitterWorkouts = crossfitterCombinedResults.CrossfitterWorkouts.ToList();

            List<RoutineComplex> childRoutines = crossfitterCombinedResults.ChildRoutines.ToList();
            List<RoutineSimple> simpleRoutingForChild = crossfitterCombinedResults.ChildRoutineSimples.ToList();
            List<RoutineSimple> routineSimples = crossfitterCombinedResults.RoutineSimples.ToList();

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
            UpdateWorkoutsWithRecords(crossfitterWorkouts);

            List<CrossfitterWorkout> allCrossfittersWorkouts = crossfitterWorkouts.OrderByDescending(x => x.Date).ThenByDescending(x => x.CreatedUtc).ToList();//.Skip(((page - 1) * pageSize)).Take(pageSize).ToList();
            foreach (CrossfitterWorkout allCrossfittersWorkout in allCrossfittersWorkouts)
            {
                allCrossfittersWorkout.RoutineComplex.Children = allCrossfittersWorkout.RoutineComplex.Children.OrderBy(x => x.Position).ToList();
            }
            return allCrossfittersWorkouts;
        }

        private void UpdateWorkoutsWithRecords(List<CrossfitterWorkout> crossfitterWorkouts)
        {
            List<string> distinctUsers = crossfitterWorkouts.GroupBy(x => x.Crossfitter.Id).Select(x => x.Key).ToList();
            foreach (string userId in distinctUsers)
            {
                List<TempPersonMaximum> personMainMaxumumsOnly =  _dapperRepository.GetPersonMainMaxumumsOnly(userId).ToList();
                List<TempPersonMaximum> previousMaximumsList =  _dapperRepository.GetPersonPreviousMainMaxumumsOnly(userId).ToList();
                UpdateRoutinesWithMaximums(crossfitterWorkouts, personMainMaxumumsOnly, previousMaximumsList);
            }
        }

        private void UpdateRoutinesWithMaximums(List<CrossfitterWorkout> crossfitterWorkouts, List<TempPersonMaximum> personMainMaxumumsOnly, List<TempPersonMaximum> previousMaximumsList)
        {
            foreach (TempPersonMaximum personMaximum in personMainMaxumumsOnly)
            {
                CrossfitterWorkout crossfitterWorkout = crossfitterWorkouts.SingleOrDefault(x => x.Id == personMaximum.CrossfitterWorkoutId);
                if (crossfitterWorkout == null)
                {
                    continue;
                }
                TempPersonMaximum previousMaximum = previousMaximumsList.FirstOrDefault(x => x.ExerciseId == personMaximum.ExerciseId);

                if (previousMaximum == null)
                {
                    personMaximum.AddedToMaxWeight =  personMaximum.MaximumWeight;
                }
                else
                {
                    personMaximum.AddedToMaxWeight = personMaximum.MaximumWeight - previousMaximum.MaximumWeight;
                }
                crossfitterWorkout.PersonalRecords.Add(personMaximum);
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
            List<PlanningHistory> planned =  _context.PlanningHistories.Where(x => x.PlanningDate.Date == today.Date).ToList();
            List<RoutineComplex> routines = _context.ComplexRoutines.Where(x => planned.SingleOrDefault(y => y.RoutineComplexId == x.Id)!= null)
                .Include(x => x.RoutineSimple)
                .ThenInclude(x => x.Exercise)
                .ThenInclude(x => x.ExerciseMeasures)
                .Include(x => x.Children)
                .ThenInclude(x => x.RoutineSimple)
                .ThenInclude(x => x.Exercise)
                .ThenInclude(x => x.ExerciseMeasures).ToList();
            routines.ForEach(x =>
                {
                    x.PlanDate = planned.Single(p => p.RoutineComplexId == x.Id).PlanningDate;
                    x.PlanningLevel = planned.Single(p => p.RoutineComplexId == x.Id).PlanningLevel;
                });
            return routines;
        }

        public List<TempPersonMaximum> GetPersonMaxumums(string userId)
        {
            return _dapperRepository.GetPersonMaxumums(userId).ToList();
        }

        public List<LeaderboardItemModel> GetLeaderboardByWorkout(int crossfitterWorkoutId)
        {
            DateTime date = _context.CrossfitterWorkouts.Single(x => x.Id == crossfitterWorkoutId).Date;
            List<RoutineComplex> getWorkoutsOnDate = GetPlannedWorkouts(date);
            List<CrossfitterWorkout> crossfitterWorkouts = _context.CrossfitterWorkouts.Where(x => getWorkoutsOnDate.SingleOrDefault(y => y.Id == x.RoutineComplexId) != null).Include(x => x.Crossfitter).ToList();
            var leaderboardResults = new List<LeaderboardItemModel>();
            foreach (RoutineComplex plannedWorkout in getWorkoutsOnDate)
            {
                IEnumerable<CrossfitterWorkout> resultsByWorkout = crossfitterWorkouts.Where(x => x.RoutineComplexId == plannedWorkout.Id);
                foreach (CrossfitterWorkout crossfitterWorkout in resultsByWorkout)
                {
                    leaderboardResults.Add(GetLeaderboardItemModel(plannedWorkout, crossfitterWorkout));
                }
            }

            return leaderboardResults;
        }

        private static LeaderboardItemModel GetLeaderboardItemModel(RoutineComplex plannedWorkout, CrossfitterWorkout crossfitterWorkout)
        {
            string result = string.Empty;
            switch (plannedWorkout.ComplexType)
            {
                case RoutineComplexType.ForTime:
                case RoutineComplexType.ForTimeManyInners:
                    if (crossfitterWorkout.RepsToFinishOnCapTime.HasValue)
                    {
                        result = $"cap + {crossfitterWorkout.RepsToFinishOnCapTime.Value}";
                    }
                    else
                    {
                        TimeSpan totalTime = crossfitterWorkout.TimePassed.Value;
                        if (totalTime.TotalHours >= 1)
                        {
                            result =  totalTime.ToString();
                        }
                        else
                        {
                            result = $"{totalTime.Minutes:00}:{totalTime.Seconds:00}";
                        }
                    }
                    break;
                case RoutineComplexType.AMRAP:
                    result = crossfitterWorkout.RoundsFinished.HasValue? $"{crossfitterWorkout.RoundsFinished.Value}": "0";
                    if (crossfitterWorkout.PartialRepsFinished.HasValue)
                    {
                        result += $" + {crossfitterWorkout.PartialRepsFinished}";
                    }
                    break;
                case RoutineComplexType.EMOM:
                    break;
                case RoutineComplexType.E2MOM:
                    break;
                case RoutineComplexType.NotForTime:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return new LeaderboardItemModel(plannedWorkout.PlanningLevel.ToString(),crossfitterWorkout.Crossfitter.UserName, result);
        }
    }
}
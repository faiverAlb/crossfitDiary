using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private readonly ReadUsersService _readUsersService;
        private readonly WorkoutsMatchDispatcher _workoutsMatchDispatcher;
        private readonly DapperRepository _dapperRepository;
        private MaximumsUpdater _maximumsUpdater;

        public ReadWorkoutsService(WorkouterContext  context
            , ReadUsersService readUsersService
            , WorkoutsMatchDispatcher workoutsMatchDispatcher
            , DapperRepository dapperRepository
            , MaximumsUpdater maximumsUpdater)
        {
            _context = context;
            _readUsersService = readUsersService;
            _workoutsMatchDispatcher = workoutsMatchDispatcher;
            _dapperRepository = dapperRepository;
            _maximumsUpdater = maximumsUpdater;
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
        ///     Returns all crossfitters workouts.
        /// </summary>
        public List<CrossfitterWorkout> GetAllCrossfittersWorkouts(string userId, int page, int pageSize)
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

            _maximumsUpdater.UpdateWorkoutsWithRecords(crossfitterWorkouts);
            _maximumsUpdater.UpdateWorkoutsWithPercentWeightCalculations(crossfitterWorkouts);

            List<CrossfitterWorkout> allCrossfittersWorkouts = crossfitterWorkouts.OrderByDescending(x => x.Date).ThenByDescending(x => x.CreatedUtc).ToList();//.Skip(((page - 1) * pageSize)).Take(pageSize).ToList();
            foreach (CrossfitterWorkout allCrossfittersWorkout in allCrossfittersWorkouts)
            {
                allCrossfittersWorkout.RoutineComplex.Children = allCrossfittersWorkout.RoutineComplex.Children.OrderBy(x => x.Position).ToList();
            }
            return allCrossfittersWorkouts;
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

        public IEnumerable<KeyValuePair<PlanningLevel, List<PlanningHistory>>> GetPlannedWorkouts(DateTime today, ApplicationUser currentUser)
        {
            List<PlanningHistory> planned =  _context
                .PlanningHistories
                .Include(x => x.RoutineComplex)
                .ThenInclude(x => x.RoutineSimple)
                .ThenInclude(x => x.Exercise)
                .ThenInclude(x => x.ExerciseMeasures)
                .Include(x => x.RoutineComplex)
                .ThenInclude(x => x.Children)
                .ThenInclude(x => x.RoutineSimple)
                .ThenInclude(x => x.Exercise)
                .ThenInclude(x => x.ExerciseMeasures)
                .AsNoTracking()
                .Where(x => x.PlanningDate.Date == today.Date)
                .ToList();
            if (planned.Any(x => x.Crossfitter == currentUser))
            {
                planned = planned.Where(x => x.Crossfitter == currentUser).ToList();
            }

            // var resultComplexes = new List<KeyValuePair<PlanningLevel, List<RoutineComplex>>>();
            IEnumerable<KeyValuePair<PlanningLevel, List<PlanningHistory>>> groupedByWodSubType = planned
                .GroupBy(x => x.PlanningLevel)
                .Select(x => new KeyValuePair<PlanningLevel, List<PlanningHistory>>(x.Key,x.ToList()));
            return groupedByWodSubType;
            // foreach (IGrouping<PlanningLevel, PlanningHistory> grouping in groupedByWodSubType)
            // {
            //     var subTypeWods = new List<RoutineComplex>();
            //     foreach (PlanningHistory planningHistory in grouping)
            //     {
            //         RoutineComplex routine = _context.ComplexRoutines
            //             .Include(x => x.RoutineSimple)
            //             .ThenInclude(x => x.Exercise)
            //             .ThenInclude(x => x.ExerciseMeasures)
            //             .Include(x => x.Children)
            //             .ThenInclude(x => x.RoutineSimple)
            //             .ThenInclude(x => x.Exercise)
            //             .ThenInclude(x => x.ExerciseMeasures)
            //             .AsNoTracking()
            //             .Single(x => x.Id == planningHistory.RoutineComplexId);
            //
            //         
            //         routine.PlanDate = planningHistory.PlanningDate;
            //         routine.PlanningLevel = planningHistory.PlanningLevel;
            //         routine.WodSubType = planningHistory.WodSubType;
            //         subTypeWods.Add(routine);
            //     }
            //
            //     resultComplexes.Add(new KeyValuePair<PlanningLevel, List<RoutineComplex>>(grouping.Key, subTypeWods));
            // }
            // return resultComplexes;
        }
        public List<RoutineComplex> GetWorkoutsList()
        {
            List<RoutineComplex> routines = _context.ComplexRoutines
                .Include(x => x.RoutineSimple)
                .ThenInclude(x => x.Exercise)
                .ThenInclude(x => x.ExerciseMeasures)
                .Include(x => x.Children)
                .ThenInclude(x => x.RoutineSimple)
                .ThenInclude(x => x.Exercise)
                .ThenInclude(x => x.ExerciseMeasures).ToList();
            return routines;
        }

        public List<LeaderboardItemModel> GetLeaderboardByWorkout(int crossfitterWorkoutId, ApplicationUser user)
        {
            //ToDo: Refactoring or remove this
        //     CrossfitterWorkout baseWorkout = _context.CrossfitterWorkouts
        //                                              .Include(x => x.RoutineComplex)
        //                                              .Include(x => x.Crossfitter)
        //                                              .Single(x => x.Id == crossfitterWorkoutId);
        //     List<RoutineComplex> workouts = GetPlannedWorkouts(baseWorkout.Date, user);
        //     if (workouts.SingleOrDefault(x => x.Id == baseWorkout.RoutineComplexId) == null)
        //     {
        //         workouts = new List<RoutineComplex>() {baseWorkout.RoutineComplex};
        //     }
        //
        //     List<CrossfitterWorkout> crossfitterWorkouts = _context.CrossfitterWorkouts
        //                                                             .Where(x => workouts.SingleOrDefault(y => y.Id == x.RoutineComplexId) != null)
        //                                                             .Include(x => x.Crossfitter)
        //                                                             .ToList();

            var leaderboardResults = new List<LeaderboardItemModel>();
            // foreach (RoutineComplex plannedWorkout in workouts)
            // {
            //     IEnumerable<CrossfitterWorkout> resultsByWorkout = crossfitterWorkouts.Where(x => x.RoutineComplexId == plannedWorkout.Id);
            //     foreach (CrossfitterWorkout crossfitterWorkout in resultsByWorkout)
            //     {
            //         leaderboardResults.Add(GetLeaderboardItemModel(plannedWorkout, crossfitterWorkout));
            //     }
            // }

            return leaderboardResults;
        }


        private LeaderboardItemModel GetLeaderboardItemModel(RoutineComplex plannedWorkout, CrossfitterWorkout crossfitterWorkout)
        {
            throw new NotImplementedException();
            // string result = string.Empty;
            // switch (plannedWorkout.ComplexType)
            // {
            //     case RoutineComplexType.ForTime:
            //     case RoutineComplexType.ForTimeManyInners:
            //         if (crossfitterWorkout.RepsToFinishOnCapTime.HasValue)
            //         {
            //             result = $"cap + {crossfitterWorkout.RepsToFinishOnCapTime.Value}";
            //         }
            //         else
            //         {
            //             if (crossfitterWorkout.TimePassed != null)
            //             {
            //                 TimeSpan totalTime = crossfitterWorkout.TimePassed.Value;
            //                 if (totalTime.TotalHours >= 1)
            //                 {
            //                     result =  totalTime.ToString();
            //                 }
            //                 else
            //                 {
            //                     result = $"{totalTime.Minutes:00}:{totalTime.Seconds:00}";
            //                 }
            //             }
            //             
            //         }
            //         break;
            //     case RoutineComplexType.AMRAP:
            //     case RoutineComplexType.AMRAPN:
            //         result = crossfitterWorkout.RoundsFinished.HasValue? $"{crossfitterWorkout.RoundsFinished.Value}": "0";
            //         if (crossfitterWorkout.PartialRepsFinished.HasValue)
            //         {
            //             result += $" + {crossfitterWorkout.PartialRepsFinished}";
            //         }
            //         break;
            //     case RoutineComplexType.EMOM:
            //         break;
            //     case RoutineComplexType.E2MOM:
            //         break;
            //     case RoutineComplexType.NotForTime:
            //         break;
            //     default:
            //         throw new ArgumentOutOfRangeException();
            // }
            // return new LeaderboardItemModel(plannedWorkout.PlanningLevel.ToString(),crossfitterWorkout.Crossfitter.FullName, result);
        }
    }
}
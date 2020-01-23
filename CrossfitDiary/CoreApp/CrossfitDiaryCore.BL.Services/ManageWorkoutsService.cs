using System;
using System.Collections.Generic;
using System.Linq;
using CrossfitDiaryCore.BL.Services.DapperStuff;
using CrossfitDiaryCore.DAL.EF;
using CrossfitDiaryCore.Model;
using CrossfitDiaryCore.Model.TempModels;
using Microsoft.EntityFrameworkCore;

namespace CrossfitDiaryCore.BL.Services
{
    public class ManageWorkoutsService
    {
        private readonly WorkouterContext _context;
        private readonly ReadWorkoutsService _readWorkoutsService;
        private readonly ReadUsersService _readUsersService;
        private readonly DapperRepository _dapperRepository;

        public ManageWorkoutsService(WorkouterContext context, ReadWorkoutsService readWorkoutsService, ReadUsersService readUsersService, DapperRepository dapperRepository)
        {
            _context = context;
            _readWorkoutsService = readWorkoutsService;
            _readUsersService = readUsersService;
            _dapperRepository = dapperRepository;
        }

        public void RemoveWorkoutResult(int crossfitterWorkoutId, string userId)
        {
            CrossfitterWorkout toRemove = _context.CrossfitterWorkouts.Include(x => x.RoutineComplex).Single(x => x.Id == crossfitterWorkoutId && x.Crossfitter.Id == userId);
            _context.CrossfitterWorkouts.Remove(toRemove);
            _context.SaveChanges();

            RemoveObsoleteWorkoutIfUserAuthor(toRemove.RoutineComplexId, userId);
        }

        /// <summary>
        ///     Verifies that workout exists, user is author and there are no logged workouts
        ///     If Ok then delete workout to cleanup DB
        /// </summary>
        /// <param name="workoutIdToCheck"></param>
        /// <param name="userId"></param>
        private void RemoveObsoleteWorkoutIfUserAuthor(int workoutIdToCheck, string userId)
        {
            RoutineComplex workoutToCheckAndDelete = _context.ComplexRoutines.Include(x => x.CreatedBy).Include(x => x.Children).SingleOrDefault(x => x.Id == workoutIdToCheck);
            if (workoutToCheckAndDelete == null)
            {
                return;
            }

            if (_context.PlanningHistories.Any(x => x.RoutineComplexId == workoutIdToCheck))
            {
                return;
            }
            if (workoutToCheckAndDelete.CreatedBy?.Id != userId)
            {
                return;
            }
            int workoutsResultsCount = _context.CrossfitterWorkouts.Count(x => x.RoutineComplexId == workoutIdToCheck);
            if (workoutsResultsCount != 0)
            {
                return;
            }

            bool inPlannedWorkouts = _context.PlanningHistories.Any(x => x.RoutineComplexId == workoutIdToCheck);
            if (inPlannedWorkouts)
            {
                return;
            }

            foreach (RoutineComplex routineComplex in workoutToCheckAndDelete.Children)
            {
                _context.ComplexRoutines.Remove(routineComplex);
            }

            _context.ComplexRoutines.Remove(workoutToCheckAndDelete);
            _context.SaveChanges();
        }



        public void UpdateWorkoutsWithPercentWeightCalculations(List<CrossfitterWorkout> crossfitterWorkouts)
        {
            var wodsWithCalculationNeeded = crossfitterWorkouts.Where(x => x.RoutineComplex.RoutineSimple.Any(y => y.WeightDisplayType != WeightDisplayType.Default)).ToList();
            List<string> distinctUsers = wodsWithCalculationNeeded.GroupBy(x => x.Crossfitter.Id).Select(x => x.Key).ToList();
            foreach (string userId in distinctUsers)
            {
                IEnumerable<TempPersonMaximum> allExercisesMaximums = _dapperRepository.GetAllMaximums(userId, DateTime.Now);
                List<CrossfitterWorkout> personWods = wodsWithCalculationNeeded.Where(x => x.Crossfitter.Id == userId).ToList();
                IEnumerable<CrossfitterWorkout> wodsWithFindMaxPM = personWods.Where(x => x.RoutineComplex.RoutineSimple.Any(y => y.WeightDisplayType == WeightDisplayType.PercentMaxPM));
                foreach (CrossfitterWorkout workout in wodsWithFindMaxPM)
                {
                    IEnumerable<RoutineSimple> exercisesWithPercentMaxPM = workout.RoutineComplex.RoutineSimple.Where(x => x.WeightDisplayType == WeightDisplayType.PercentMaxPM);
                    foreach (RoutineSimple routineSimple in exercisesWithPercentMaxPM)
                    {

                        decimal? calculatedMaximumWeight = GetMaxValue(allExercisesMaximums, routineSimple.ExerciseId, workout.Date);
                        routineSimple.CalculateWeight(calculatedMaximumWeight);
                    }
                }

                IEnumerable<CrossfitterWorkout> wodsWithFindPrevPM = personWods.Where(x => x.RoutineComplex.RoutineSimple.Any(y => y.WeightDisplayType == WeightDisplayType.PercentPreviousPM));
                foreach (CrossfitterWorkout workout in wodsWithFindPrevPM)
                {
                    IEnumerable<RoutineSimple> exercisesWithPercentPreviousPM = workout.RoutineComplex.RoutineSimple.Where(x => x.WeightDisplayType == WeightDisplayType.PercentPreviousPM);

                    foreach (RoutineSimple routineSimple in exercisesWithPercentPreviousPM)
                    {
                        CrossfitterWorkout wod = _dapperRepository.GetCrossfiterWodWithFindMaxWeight(userId, workout.Date, routineSimple.ExerciseId);

                        if (wod == null)
                        {
                            decimal? calculatedMaximumWeight = GetMaxValue(allExercisesMaximums, routineSimple.ExerciseId, workout.Date);
                            routineSimple.CalculateWeight(calculatedMaximumWeight);
                        }
                        else
                        {
                            routineSimple.CalculateWeight(wod.Weight);
                        }

                    }
                }
            }

        }

        private decimal? GetMaxValue(IEnumerable<TempPersonMaximum> allExercisesMaximums, int exerciseId,  DateTime workoutDate)
        {
            IEnumerable<TempPersonMaximum> foundAllExerciseMaximums = allExercisesMaximums
                .Where(x => x.ExerciseId == exerciseId && x.Date <= workoutDate)
                .OrderByDescending(x => x.CalculatedMaximumWeight)
                .ThenBy(x => x.Date);
            return foundAllExerciseMaximums.FirstOrDefault()?.CalculatedMaximumWeight;
        }



        public void CreateAndLogNewWorkout(RoutineComplex workoutRoutine, CrossfitterWorkout logWorkoutModel, ApplicationUser user)
        {
            //todo: precheck rights for workout + log
            CalculateWeightBasedOnWeightsPercent(workoutRoutine, user.Id, logWorkoutModel.Date);

            int workoutId = _readWorkoutsService.FindDefaultOrExistingWorkout(workoutRoutine);

            int currentWorkoutIdFromUI = -1;
            if (workoutId == 0)
            {
                currentWorkoutIdFromUI = workoutRoutine.Id;
                workoutRoutine.Id = 0;
                // workoutRoutine.PlanDate = null;
                // workoutRoutine.PlanningLevel = null;
                int childIndex = 0;
                foreach (RoutineComplex routineChild in workoutRoutine.Children)
                {
                    routineChild.Id = 0;
                    routineChild.Position = childIndex++;
                }

                int index = 0;
                foreach (RoutineSimple routineSimple in workoutRoutine.RoutineSimple)
                {
                    routineSimple.Position = index++;
                }
                _context.ComplexRoutines.Add(workoutRoutine);
                _context.SaveChanges();
                workoutId = workoutRoutine.Id;
            }

            CrossfitterWorkout foundCrossfitterWorkout =  _context.CrossfitterWorkouts.SingleOrDefault(x => x.Crossfitter == user && x.Id == logWorkoutModel.Id);
            if (foundCrossfitterWorkout != null)
            {
                _context.CrossfitterWorkouts.Remove(foundCrossfitterWorkout);
                _context.SaveChanges();

                RemoveObsoleteWorkoutIfUserAuthor(currentWorkoutIdFromUI, user.Id);
            }
            logWorkoutModel.RoutineComplexId = workoutId;
            logWorkoutModel.Id = 0;
            _context.CrossfitterWorkouts.Add(logWorkoutModel);
            _context.SaveChanges();
        }

        private void CalculateWeightBasedOnWeightsPercent(RoutineComplex workoutRoutine, string userId, DateTime date)
        {
            IEnumerable<TempPersonMaximum> allExercisesMaximums = _dapperRepository.GetAllMaximums(userId, DateTime.Now);
            foreach (RoutineSimple routineSimple in workoutRoutine.RoutineSimple)
            {
                switch (routineSimple.WeightDisplayType)
                {
                    case WeightDisplayType.Default:
                        break;
                    case WeightDisplayType.PercentPreviousPM:
                        CrossfitterWorkout wod = _dapperRepository.GetCrossfiterWodWithFindMaxWeight(userId, date, routineSimple.ExerciseId);
                        if (wod == null)
                        {
                            decimal? calculatedMaximumWeight = GetMaxValue(allExercisesMaximums, routineSimple.ExerciseId, date);
                            routineSimple.CalculateWeight(calculatedMaximumWeight);
                        }
                        else
                        {
                            routineSimple.CalculateWeight(wod.Weight);
                        }
                        break;
                    case WeightDisplayType.PercentMaxPM:
                        decimal? findPrevMaxWeightInFindMaxWods = GetMaxValue(allExercisesMaximums,routineSimple.ExerciseId, date);
                        routineSimple.CalculateWeight(findPrevMaxWeightInFindMaxWods);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            }
        }


        public void PlanWorkoutForDay(int workoutId, PlanningLevel planningLevel, DateTime date, ApplicationUser user, WodSubType wodSubType)
        {
            var historyItem = new PlanningHistory()
            {
                WodSubType = wodSubType,
                RoutineComplexId = workoutId,
                Crossfitter = user,
                PlanningLevel = planningLevel,
                PlanningDate = date
            };
            PlanWorkoutForDay(historyItem);
        }

        private void PlanWorkoutForDay(PlanningHistory newHistoryPlanning)
        {
            IEnumerable<PlanningHistory> planningHistories =  _context.PlanningHistories.Where(x => x.Id == newHistoryPlanning.Id);
            _context.PlanningHistories.RemoveRange(planningHistories);
            _context.PlanningHistories.Add(newHistoryPlanning);
            _context.SaveChanges();
        }



        public void PlanWorkout(PlanningHistory newHistoryPlanning, ApplicationUser user)
        {
            RoutineComplex workoutRoutine = newHistoryPlanning.RoutineComplex;
            int workoutId = _readWorkoutsService.FindDefaultOrExistingWorkout(workoutRoutine);
            // CleanPreviousPlannedWodsForThisLevel(workoutId, workoutRoutine.PlanningLevel, workoutRoutine.PlanDate.Value.Date);
            
            if (workoutId == 0)
            {
                if (workoutRoutine.Id != -1)
                {
                    RemoveObsoleteWorkoutIfUserAuthor(workoutRoutine.Id, user.Id);
                }

                workoutRoutine.Id = 0;
                int childIndex = 0;
                foreach (RoutineComplex routineChild in workoutRoutine.Children)
                {
                    routineChild.Id = 0;
                    routineChild.Position = childIndex++;
                }

                int index = 0;
                foreach (RoutineSimple routineSimple in workoutRoutine.RoutineSimple)
                {
                    routineSimple.Position = index++;
                }
                _context.ComplexRoutines.Add(workoutRoutine);
                _context.PlanningHistories.Add(newHistoryPlanning);
                _context.SaveChanges();
            }
            else
            {
                PlanWorkoutForDay(newHistoryPlanning);
            }
        }

        public void LogNewWorkout(CrossfitterWorkout crossfitterWorkout)
        {
            _context.CrossfitterWorkouts.Add(crossfitterWorkout);
            _context.SaveChanges();
        }

        public void RemovePlannedWod(int plannedWodId, string userId, DateTime date)
        {
            bool canUserPlanPublicWods = _readUsersService.CanUserPlanWorkouts(userId);
            PlanningHistory foundPlannedWodForDate = _context.PlanningHistories.Single(x => x.Id == plannedWodId);
            if (foundPlannedWodForDate.Crossfitter == null && canUserPlanPublicWods == false)
            {
                return;
            }
            if (foundPlannedWodForDate.Crossfitter == null && canUserPlanPublicWods)
            {
                _context.PlanningHistories.Remove(foundPlannedWodForDate);
                _context.SaveChanges();
            }

            if (foundPlannedWodForDate.Crossfitter != null && foundPlannedWodForDate.Crossfitter.Id == userId)
            {
                _context.PlanningHistories.Remove(foundPlannedWodForDate);
                _context.SaveChanges();
            }

        }

    }
}
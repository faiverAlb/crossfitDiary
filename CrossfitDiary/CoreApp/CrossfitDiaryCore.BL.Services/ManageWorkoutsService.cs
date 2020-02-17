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
        private readonly MaximumsUpdater _maximumsUpdater;

        public ManageWorkoutsService(WorkouterContext context, ReadWorkoutsService readWorkoutsService, ReadUsersService readUsersService, DapperRepository dapperRepository, MaximumsUpdater maximumsUpdater)
        {
            _context = context;
            _readWorkoutsService = readWorkoutsService;
            _readUsersService = readUsersService;
            _dapperRepository = dapperRepository;
            _maximumsUpdater = maximumsUpdater;
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


        public void CreateAndLogNewWorkout(RoutineComplex workoutRoutine, CrossfitterWorkout logWorkoutModel, ApplicationUser user)
        {
            //todo: precheck rights for workout + log
            _maximumsUpdater.CalculateWeightBasedOnWeightsPercent(workoutRoutine, user.Id, logWorkoutModel.Date);

            int workoutId = _readWorkoutsService.FindDefaultOrExistingWorkoutNonTracking(workoutRoutine);

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
            PlanWorkoutForDay(historyItem, user);
        }

        private void PlanWorkoutForDay(PlanningHistory newHistoryPlanning, ApplicationUser applicationUser)
        {
            if (newHistoryPlanning.IsPrivatePlanning == false && applicationUser.CanPlanWorkouts == false)
            {
                newHistoryPlanning.IsPrivatePlanning = true;
            }

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
                newHistoryPlanning.RoutineComplexId = workoutId;
                newHistoryPlanning.RoutineComplex = _readWorkoutsService.GetWorkout(workoutId);
                PlanWorkoutForDay(newHistoryPlanning, user);
            }
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
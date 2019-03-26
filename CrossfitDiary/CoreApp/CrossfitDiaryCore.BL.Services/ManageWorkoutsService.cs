﻿using System;
using System.Collections.Generic;
using System.Linq;
using CrossfitDiaryCore.DAL.EF;
using CrossfitDiaryCore.Model;
using Microsoft.EntityFrameworkCore;

namespace CrossfitDiaryCore.BL.Services
{
    public class ManageWorkoutsService
    {
        private readonly WorkouterContext _context;
        private readonly ReadWorkoutsService _readWorkoutsService;

        public ManageWorkoutsService(WorkouterContext context, ReadWorkoutsService readWorkoutsService)
        {
            _context = context;
            _readWorkoutsService = readWorkoutsService;
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

            if (workoutToCheckAndDelete.PlanDate.HasValue)
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
            int workoutId = _readWorkoutsService.FindDefaultOrExistingWorkout(workoutRoutine);
            int currentWorkoutIdFromUI = -1;
            if (workoutId == 0)
            {
                currentWorkoutIdFromUI = workoutRoutine.Id;
                workoutRoutine.Id = 0;
                workoutRoutine.PlanDate = null;
                workoutRoutine.PlanningLevel = null;
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

        public void PlanWorkout(RoutineComplex workoutRoutine, ApplicationUser user)
        {
            int workoutId = _readWorkoutsService.FindDefaultOrExistingWorkout(workoutRoutine);

            List<RoutineComplex> sameDayPlanned = _context.PlanningHistories.Where(x => x.PlanningDate);

            List<RoutineComplex> sameDayPlanned = _context.ComplexRoutines.Where(x =>
                x.PlanDate != null && x.PlanDate.Value.Date == workoutRoutine.PlanDate.Value.Date &&
                x.PlanningLevel != null && x.PlanningLevel == workoutRoutine.PlanningLevel).ToList();
            foreach (RoutineComplex routineComplex in sameDayPlanned)
            {
                routineComplex.PlanDate = null;
                routineComplex.PlanningLevel = null;
            }

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
                _context.SaveChanges();
            }
            else
            {
                RoutineComplex complexToUpdate = _context.ComplexRoutines.Single(x => x.Id == workoutId);
                complexToUpdate.PlanDate = workoutRoutine.PlanDate;
                complexToUpdate.PlanningLevel = workoutRoutine.PlanningLevel;
                _context.ComplexRoutines.Update(complexToUpdate);
                _context.SaveChanges();

            }
        }

        public void LogNewWorkout(CrossfitterWorkout crossfitterWorkout)
        {

            _context.CrossfitterWorkouts.Add(crossfitterWorkout);
            _context.SaveChanges();
        }
    }
}
using System;
using System.Linq;
using CrossfitDiaryCore.DAL.EF;
using CrossfitDiaryCore.Model;

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

        public void RemoveWorkout(int crossfitterWorkoutId, string userId)
        {
            CrossfitterWorkout toRemove = _context.CrossfitterWorkouts.Single(x => x.Id == crossfitterWorkoutId && x.Crossfitter.Id == userId);
            _context.CrossfitterWorkouts.Remove(toRemove);
            _context.SaveChanges();
        }

        public void CreateAndLogNewWorkout(RoutineComplex newWorkoutRoutine, CrossfitterWorkout logWorkoutModel)
        {
            int newWorkoutId = _readWorkoutsService.FindDefaultOrExistingWorkout(newWorkoutRoutine);
            bool isEditMode = newWorkoutId != 0;
            if (newWorkoutId == 0)
            {
                _context.ComplexRoutines.Add(newWorkoutRoutine);
                _context.SaveChanges();
                newWorkoutId = newWorkoutRoutine.Id;
            }

            logWorkoutModel.RoutineComplexId = newWorkoutId;
            LogWorkout(logWorkoutModel, isEditMode);
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
        private void LogWorkout(CrossfitterWorkout workoutToLog, bool isEditMode)
        {
            if (isEditMode)
            {
                _context.CrossfitterWorkouts.Remove(_context.CrossfitterWorkouts.Single(x => x.Id == workoutToLog.Id));
                _context.SaveChanges();
                workoutToLog.Id = 0;
            }
            _context.CrossfitterWorkouts.Add(workoutToLog);
            _context.SaveChanges();
        }


        public int CreateWorkout(RoutineComplex routineComplexToSave)
        {
            int workoutId = _readWorkoutsService.FindDefaultOrExistingWorkout(routineComplexToSave);
            if (workoutId != 0)
            {
                return workoutId;
            }

            _context.ComplexRoutines.Add(routineComplexToSave);
            _context.SaveChanges();
            //            _routineComplexRepository.Add(routineComplexToSave);
            //            _unitOfWork.Commit();
            return routineComplexToSave.Id;
        }


    }
}
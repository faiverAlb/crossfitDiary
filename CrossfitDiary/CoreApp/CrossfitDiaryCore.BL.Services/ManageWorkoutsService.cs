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

        public void CreateAndLogNewWorkout(RoutineComplex workoutRoutine, CrossfitterWorkout logWorkoutModel, ApplicationUser user)
        {
            //todo: precheck rights for workout + log
            int workoutId = _readWorkoutsService.FindDefaultOrExistingWorkout(workoutRoutine);
            if (workoutId == 0)
            {
                workoutRoutine.Id = 0;
                foreach (RoutineComplex routineChild in workoutRoutine.Children)
                {
                    routineChild.Id = 0;
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
            }
            logWorkoutModel.RoutineComplexId = workoutId;
            logWorkoutModel.Id = 0;
            _context.CrossfitterWorkouts.Add(logWorkoutModel);
            _context.SaveChanges();
        }
    }
}
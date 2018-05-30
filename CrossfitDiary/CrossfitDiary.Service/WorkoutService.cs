using System.Collections.Generic;
using System.Threading;
using CrossfitDiary.DAL.EF.Repositories;
using CrossfitDiary.Model;
using CrossfitDiary.Service.Interfaces;

namespace CrossfitDiary.Service
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IRoutineComplexRepository _routineComplexRepository;

        public WorkoutService(IRoutineComplexRepository routineComplexRepository)
        {
            _routineComplexRepository = routineComplexRepository;
        }

        public IEnumerable<RoutineComplex> GetAllWorkouts()
        {
            IEnumerable<RoutineComplex> availableWorkouts = _routineComplexRepository.GetAll();
            return availableWorkouts;
        }

        public RoutineComplex GetWorkout(int workoutId)
        {
            return _routineComplexRepository.SingleOrDefault(x => x.Id == workoutId);
        }
    }
}
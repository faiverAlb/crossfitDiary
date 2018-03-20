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

        public IEnumerable<RoutineComplex> GetDefaultAndUserWorkouts(string userId)
        {
            IEnumerable<RoutineComplex> availableWorkouts = _routineComplexRepository.GetMany(x => x.CreatedBy == null || x.CreatedBy.Id == userId);
            return availableWorkouts;
        }

    }
}
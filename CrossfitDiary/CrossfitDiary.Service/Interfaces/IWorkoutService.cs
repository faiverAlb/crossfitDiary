using System.Collections.Generic;
using CrossfitDiary.Model;

namespace CrossfitDiary.Service.Interfaces
{
    public interface IWorkoutService
    {
        /// <summary>
        /// Return all workouts.
        /// </summary>
        /// <returns>
        /// </returns>
        IEnumerable<RoutineComplex> GetAllWorkouts();
    }
}
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

        /// <summary>
        ///     Returns workout by workout id
        /// </summary>
        /// <param name="workoutId"></param>
        /// <returns></returns>
        RoutineComplex GetWorkout(int workoutId);
    }
}
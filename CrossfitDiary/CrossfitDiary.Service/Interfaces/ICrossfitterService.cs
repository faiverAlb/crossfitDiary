using System;
using System.Collections.Generic;
using CrossfitDiary.Model;

namespace CrossfitDiary.Service.Interfaces
{
    public interface ICrossfitterService
    {
        int CreateWorkout(RoutineComplex map);
        void LogWorkout(CrossfitterWorkout map);
        void CreateAndLogNewWorkout(RoutineComplex newWorkoutRoutine, CrossfitterWorkout logWorkoutModel);

        List<CrossfitterWorkout> GetCrossfitterWorkouts(string userId, DateTime fromDate, DateTime dueInterval);
    }
}
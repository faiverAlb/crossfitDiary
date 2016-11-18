using System.Collections.Generic;
using CrossfitDiary.Model;

namespace CrossfitDiary.Service.Interfaces
{
    public interface IWorkoutService
    {
        IEnumerable<RoutineComplex> GetAvailableWorkouts();
    }
}
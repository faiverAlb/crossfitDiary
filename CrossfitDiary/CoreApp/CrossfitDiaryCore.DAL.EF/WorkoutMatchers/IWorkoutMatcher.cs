using CrossfitDiaryCore.Model;

namespace CrossfitDiaryCore.DAL.EF.WorkoutMatchers
{
    /// <summary>
    ///     Interface for any workouts matchers to compare two routine complexes
    /// </summary>
    public interface IWorkoutMatcher
    {
        bool IsWorkoutMatch(RoutineComplex firstRoutineComplex, RoutineComplex secondRoutineComplex);
    }
}
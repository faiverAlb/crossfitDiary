using CrossfitDiaryCore.Model;

namespace CrossfitDiaryCore.BL.Services.WorkoutMatchers
{
    /// <summary>
    ///     Compare children workout properties
    /// </summary>
    public class ChildrenWorkoutsMatcher: IWorkoutMatcher
    {
        public bool IsWorkoutMatch(RoutineComplex firstRoutineComplex, RoutineComplex secondRoutineComplex)
        {
            return firstRoutineComplex.Children.Count == secondRoutineComplex.Children.Count;
        }
    }
}
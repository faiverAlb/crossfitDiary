using CrossfitDiary.Model;
using CrossfitDiary.Service.Interfaces;

namespace CrossfitDiary.Service.WorkoutMatchers
{
    /// <summary>
    ///     Compare general properties of workouts
    /// </summary>
    public class ComplexParametersMatcher: IWorkoutMatcher
    {
        public bool IsWorkoutMatch(RoutineComplex firstRoutineComplex, RoutineComplex secondRoutineComplex)
        {
            if (firstRoutineComplex.ComplexType != secondRoutineComplex.ComplexType)
            {
                return false;
            }

            if (firstRoutineComplex.RoundCount != secondRoutineComplex.RoundCount)
            {
                return false;
            }

            if (firstRoutineComplex.TimeToWork != secondRoutineComplex.TimeToWork)
            {
                return false;
            }

            if (firstRoutineComplex.TimeCap != secondRoutineComplex.TimeCap)
            {
                return false;
            }

            if (firstRoutineComplex.RestBetweenRounds != secondRoutineComplex.RestBetweenRounds)
            {
                return false;
            }

            return true;

        }
    }
}
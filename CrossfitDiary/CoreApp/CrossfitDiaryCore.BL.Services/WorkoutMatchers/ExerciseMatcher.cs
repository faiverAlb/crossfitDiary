using System.Linq;
using CrossfitDiaryCore.Model;

namespace CrossfitDiaryCore.BL.Services.WorkoutMatchers
{
    /// <summary>
    ///     Compare all exercises between two complex routines
    /// </summary>
    public class ExerciseMatcher: IWorkoutMatcher
    {
        public bool IsWorkoutMatch(RoutineComplex firstRoutineComplex, RoutineComplex secondRoutineComplex)
        {
            if (firstRoutineComplex.RoutineSimple.Count != secondRoutineComplex.RoutineSimple.Count)
            {
                return false;
            }

            firstRoutineComplex.RoutineSimple = firstRoutineComplex.RoutineSimple.OrderBy(x => x.Position).ToList();
            secondRoutineComplex.RoutineSimple = secondRoutineComplex.RoutineSimple.OrderBy(x => x.Position).ToList();
            for (int i = 0; i < firstRoutineComplex.RoutineSimple.Count; i++)
            {
                RoutineSimple routineSimpleToSave = firstRoutineComplex.RoutineSimple.ToList()[i];
                RoutineSimple existingSimpleRoutine = secondRoutineComplex.RoutineSimple.ToList()[i];
                if (routineSimpleToSave.ExerciseId != existingSimpleRoutine.ExerciseId
                    || routineSimpleToSave.Count != existingSimpleRoutine.Count
                    || routineSimpleToSave.Distance != existingSimpleRoutine.Distance
                    || routineSimpleToSave.Weight != existingSimpleRoutine.Weight
                    || routineSimpleToSave.AlternativeWeight != existingSimpleRoutine.AlternativeWeight
                    || routineSimpleToSave.Calories != existingSimpleRoutine.Calories
                    || routineSimpleToSave.Centimeters != existingSimpleRoutine.Centimeters
                    || routineSimpleToSave.Seconds != existingSimpleRoutine.Seconds
                    || routineSimpleToSave.IsDoUnbroken != existingSimpleRoutine.IsDoUnbroken
                    || routineSimpleToSave.TimeToWork != existingSimpleRoutine.TimeToWork
                    || routineSimpleToSave.IsAlternative != existingSimpleRoutine.IsAlternative
                    || routineSimpleToSave.WeightDisplayType != existingSimpleRoutine.WeightDisplayType
                    || routineSimpleToSave.WeightPercentValue != existingSimpleRoutine.WeightPercentValue)

                {
                    return false;
                }
            }
            return true;

        }
    }
}
﻿using System.Linq;
using CrossfitDiary.Model;
using CrossfitDiary.Service.Interfaces;

namespace CrossfitDiary.Service.WorkoutMatchers
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

            for (int i = 0; i < firstRoutineComplex.RoutineSimple.Count; i++)
            {
                RoutineSimple routineSimpleToSave = firstRoutineComplex.RoutineSimple.ToList()[i];
                RoutineSimple existingSimpleRoutine = secondRoutineComplex.RoutineSimple.ToList()[i];
                if (routineSimpleToSave.ExerciseId != existingSimpleRoutine.ExerciseId
                    || routineSimpleToSave.Count != existingSimpleRoutine.Count
                    || routineSimpleToSave.Distance != existingSimpleRoutine.Distance
                    || routineSimpleToSave.Weight != existingSimpleRoutine.Weight
                    || routineSimpleToSave.Calories != existingSimpleRoutine.Calories
                    || routineSimpleToSave.Centimeters != existingSimpleRoutine.Centimeters)
                {
                    return false;
                }
            }
            return true;

        }
    }
}
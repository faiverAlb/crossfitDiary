using System.Collections.Generic;
using System.Linq;
using CrossfitDiaryCore.Model;

namespace CrossfitDiaryCore.BL.Services.WorkoutMatchers
{
    /// <summary>
    ///     Represents general point to compare to routine complexes
    /// </summary>
    public class WorkoutsMatchDispatcher
    {
        private readonly IEnumerable<IWorkoutMatcher> _matcherList;

        public WorkoutsMatchDispatcher()
        {
            _matcherList = new IWorkoutMatcher[]
            {
                new ExerciseMatcher(),
                new ComplexParametersMatcher(), 
                new ChildrenWorkoutsMatcher()
            };
        }

        /// <summary>
        ///     Validates two routine complexes in front of all matchers
        /// </summary>
        /// <param name="firstComplex"></param>
        /// <param name="secondComplex"></param>
        /// <returns></returns>
        public bool IsWorkoutsMatch(RoutineComplex firstComplex, RoutineComplex secondComplex)
        {
            foreach (IWorkoutMatcher workoutMatch in _matcherList)
            {
                if (workoutMatch.IsWorkoutMatch(firstComplex,secondComplex) == false)
                {
                    return false;
                }

                if (IsChildrenWorkoutParamsMatch(firstComplex, secondComplex) == false)
                {
                    return false;
                }

            }

            return true;
        }

        /// <summary>
        ///     Compare inner workouts
        /// </summary>
        /// <param name="routineComplexToSave"></param>
        /// <param name="existingRoutineComplex"></param>
        /// <returns></returns>
        private bool IsChildrenWorkoutParamsMatch(RoutineComplex routineComplexToSave, RoutineComplex existingRoutineComplex)
        {
            List<RoutineComplex> existingChilds = existingRoutineComplex.OrderedChildren.ToList();
            List<RoutineComplex> toSaveChilds = routineComplexToSave.Children.ToList();

            for (var i = 0; i < existingChilds.Count; i++)
            {
                RoutineComplex existingChild = existingChilds[i];
                RoutineComplex routineComplex = toSaveChilds[i];

                if (IsWorkoutsMatch(existingChild, routineComplex) == false)
                {
                    return false;
                }
            }

            return true;
        }


    }
}
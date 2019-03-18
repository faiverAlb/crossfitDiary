using System.Collections.Generic;

namespace CrossfitDiaryCore.Model.TempModels
{
    public class DapperResults {
        public IEnumerable<CrossfitterWorkout> CrossfitterWorkouts { get; }
        public IEnumerable<RoutineSimple> RoutineSimples { get; }
        public IEnumerable<RoutineComplex> ChildRoutines { get; }
        public IEnumerable<RoutineSimple> ChildRoutineSimples { get; }

        

        public DapperResults()
        {

        }

        public DapperResults(IEnumerable<CrossfitterWorkout> crossfitterWorkouts, IEnumerable<RoutineSimple> routineSimples, IEnumerable<RoutineComplex> routineComplex, IEnumerable<RoutineSimple> childRoutineSimples)
        {
            CrossfitterWorkouts = crossfitterWorkouts;
            RoutineSimples = routineSimples;
            ChildRoutines = routineComplex;
            ChildRoutineSimples = childRoutineSimples;
        }
    }
}
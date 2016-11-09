using System.Collections.Generic;
using CrossfitDiary.Model;

namespace CrossfitDiary.Service.Interfaces
{
    public interface IExerciseService
    {
        IEnumerable<Exercise> GetExercises(string title = null);
        Exercise GetExercise(int id);

        void CreateExercise(Exercise exercise);
        void SaveExercise();
    }
}
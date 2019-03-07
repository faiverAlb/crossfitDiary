using System.Collections.Generic;
using System.Linq;
using CrossfitDiaryCore.DAL.EF;
using CrossfitDiaryCore.Model;

namespace CrossfitDiaryCore.Web
{
    public class DbInitializer
    {
        public static void Seed(WorkouterContext context)
        {
            AddExerciseIfPossible("L-Sit Hold", "L-Sit Hold", new List<ExerciseMeasure> { }, context);
            AddExerciseIfPossible("L-Sit Rope Climb", "L-Sit RC", new List<ExerciseMeasure> { new ExerciseMeasure(){ExerciseMeasureTypeId = MeasureType.Count}}, context);
            AddExerciseIfPossible("Rope Climb from floor", "RC from floor", new List<ExerciseMeasure> { new ExerciseMeasure(){ExerciseMeasureTypeId = MeasureType.Count } }, context);
            AddExerciseIfPossible("Rope Climb legless", "RC legless", new List<ExerciseMeasure> { new ExerciseMeasure(){ExerciseMeasureTypeId = MeasureType.Count } }, context);
            AddExerciseIfPossible("Lateral step up box", "Lateral step up box", new List<ExerciseMeasure> { new ExerciseMeasure(){ExerciseMeasureTypeId = MeasureType.Count }, new ExerciseMeasure() { ExerciseMeasureTypeId = MeasureType.Height } }, context);
        }

        private static void AddExerciseIfPossible(string title, string abbreviation, List<ExerciseMeasure> exerciseMeasures, WorkouterContext context)
        {
            if (context.Exercises.SingleOrDefault(x => x.Title.ToLower() == title.ToLower()) == null)
            {
                context.Exercises.Add(new Exercise()
                {
                    Abbreviation = abbreviation,
                    Title = title,
                    ExerciseMeasures = exerciseMeasures
                });
                context.SaveChanges();
            }

        }
    }
}
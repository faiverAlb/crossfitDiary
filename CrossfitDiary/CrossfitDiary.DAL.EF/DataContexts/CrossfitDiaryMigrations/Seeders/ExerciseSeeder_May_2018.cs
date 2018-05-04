using System.Collections.Generic;
using System.Linq;
using CrossfitDiary.Model;

namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations.Seeders
{
    public static partial class ExerciseSeeder
    {

        internal static List<Exercise> AddSeeds_May_2018(CrossfitDiaryDbContext context)
        {
            if (IsExerciseAlreadyExist("Glute-ham developer", context))
            {
                return new List<Exercise>();
            }

            return new List<Exercise>
            {
                new Exercise {Title = "Glute-ham developer", Abbreviation = "GHD", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
                new Exercise {Title = "Jumping pull-ups", Abbreviation = "Jumping PU", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count)},
                new Exercise {Title = "Kettlebell Front Squat", Abbreviation = "KB FS", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)}
            };
        }
        internal static List<Exercise> AddSeeds_May_2018_Second(CrossfitDiaryDbContext context)
        {
            if (IsExerciseAlreadyExist("Pull Snatch", context))
            {
                return new List<Exercise>();
            }

            return new List<Exercise>
            {
                new Exercise {Title = "Pull Snatch", Abbreviation = "Pull Snatch", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
            };
        }


        internal static List<Exercise> UpdateHeightMeasures(CrossfitDiaryDbContext context)
        {
            if (context.Exercises.Any(x => x.Title.ToLower() == "Box Jumps".ToLower() && x.ExerciseMeasures.FirstOrDefault(y => y.ExerciseMeasureType.MeasureType == MeasureType.Height) != null))
            {
                return new List<Exercise>();
            }

            var result = new List<Exercise>();

            Exercise boxJump = context.Exercises.Single(x => x.Title == "Box Jumps" && x.Abbreviation == "BJ");
            Exercise burpeeBoxJump = context.Exercises.Single(x => x.Title == "Burpee box jump" && x.Abbreviation == "Burpee BJ");
            Exercise burpeeBoxJumpOver = context.Exercises.Single(x => x.Title == "Burpee box jump over" && x.Abbreviation == "Burpee BJ Ov");

            ExerciseMeasureType exerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Height);
            AddMeasureToExercise(boxJump, exerciseMeasureType);
            AddMeasureToExercise(burpeeBoxJump, exerciseMeasureType);
            AddMeasureToExercise(burpeeBoxJumpOver, exerciseMeasureType);

            result.AddRange(new[] {boxJump, burpeeBoxJump, burpeeBoxJumpOver});
            return result;
        }

        private static void AddMeasureToExercise(Exercise exercise, ExerciseMeasureType exerciseMeasureType)
        {
            exercise.ExerciseMeasures.Add(new ExerciseMeasure()
            {
                Exercise = exercise,
                ExerciseMeasureType = exerciseMeasureType,
            });

        }
    }
}
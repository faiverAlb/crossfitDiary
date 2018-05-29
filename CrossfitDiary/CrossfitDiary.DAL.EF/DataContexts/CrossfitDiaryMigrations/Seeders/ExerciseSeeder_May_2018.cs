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

        internal static List<Exercise> AddSeeds_May_2018_Third(CrossfitDiaryDbContext context)
        {
            if (IsExerciseAlreadyExist("Push Press(Db/Kb) arm", context))
            {
                return new List<Exercise>();
            }

            return new List<Exercise>
            {
                new Exercise {Title = "Push Press(Db/Kb)arm", Abbreviation = "Push Press(Db/Kb)arm", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
                new Exercise {Title = "Push Jerk(Db/Kb)", Abbreviation = "PJ(Db/Kb)", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
                new Exercise {Title = "Power Clean(DB/KB)", Abbreviation = "PC(DB/KB)", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
                new Exercise {Title = "Power Snatch(DB/KB)", Abbreviation = "PS(DB/KB)", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
                new Exercise {Title = "Thruster(DB/KB)", Abbreviation = "Thruster(DB/KB)", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
            };
        }
        internal static List<Exercise> AddSeeds_May_2018_Fourth(CrossfitDiaryDbContext context)
        {
            if (IsExerciseAlreadyExist("DB/KB box step over", context))
            {
                return new List<Exercise>();
            }

            return new List<Exercise>
            {
                new Exercise {Title = "DB/KB box step over", Abbreviation = "DB/KB box step over", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight,MeasureType.Height)},
                new Exercise {Title = "DB/KB box step up", Abbreviation = "DB/KB box step up", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight,MeasureType.Height)},
                new Exercise {Title = "1 arm DB overhead lunges", Abbreviation = "1 arm DB overhead lunges", ExerciseMeasures = GetExerciseMeasures(context,  MeasureType.Weight, MeasureType.Distance)},
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
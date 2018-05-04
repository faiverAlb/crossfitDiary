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
                new Exercise {Title = "Pull Snatch", Abbreviation = "PSN", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
                new Exercise {Title = "Kettlebell Front Squat", Abbreviation = "KB FS", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)}
            };
        }

    }
}
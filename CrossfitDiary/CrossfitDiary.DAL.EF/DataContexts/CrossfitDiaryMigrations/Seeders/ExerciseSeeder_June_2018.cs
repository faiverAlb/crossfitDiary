using System.Collections.Generic;
using System.Linq;
using CrossfitDiary.Model;

namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations.Seeders
{
    public static partial class ExerciseSeeder
    {

        internal static List<Exercise> AddSeeds_June_2018_Fourth(CrossfitDiaryDbContext context)
        {
            if (IsExerciseAlreadyExist("Barbell overhead lunges", context))
            {
                return new List<Exercise>();
            }

            return new List<Exercise>
            {
                new Exercise {Title = "Barbell overhead lunges", Abbreviation = "Barbell OH lunges", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Weight, MeasureType.Distance)},
            };
        }
        internal static List<Exercise> AddSeeds_June_2018_21(CrossfitDiaryDbContext context)
        {
            if (IsExerciseAlreadyExist("Squat Snatch", context))
            {
                return new List<Exercise>();
            }

            return new List<Exercise>
            {
                new Exercise {Title = "Squat Snatch", Abbreviation = "Squat SN", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count,MeasureType.Weight)},
            };
        }
        internal static List<Exercise> AddSeeds_June_2018_22(CrossfitDiaryDbContext context)
        {
            if (IsExerciseAlreadyExist("Ring Muscle-Ups (strict)", context))
            {
                return new List<Exercise>();
            }

            return new List<Exercise>
            {
                new Exercise {Title = "Ring Muscle-Ups (strict)", Abbreviation = "RMU (strict)", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count)},
                new Exercise {Title = "Shuttle run", Abbreviation = "Shuttle run", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Distance)},
                new Exercise {Title = "Shuttle run (banded)", Abbreviation = "Shuttle run (banded)", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Distance)},
            };
        }
    }
}
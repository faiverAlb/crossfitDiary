using System.Collections.Generic;
using System.Linq;
using CrossfitDiary.Model;

namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations.Seeders
{
    public static partial class ExerciseSeeder
    {

        private static bool IsExerciseAlreadyExist(string exerciseTitle, CrossfitDiaryDbContext context)
        {
            return context.Exercises.Any(x => x.Title.ToLower() == exerciseTitle.ToLower());
        }

        internal static List<Exercise> AddSeeds_February_2018(CrossfitDiaryDbContext context)
        {
            if (IsExerciseAlreadyExist("Handstand Push-Ups (strict)", context))
            {
                return new List<Exercise>();
            }

            return new List<Exercise>
            {
                new Exercise {Title = "Handstand Push-Ups (strict)", Abbreviation = "HSPU(strict)", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count)},
                new Exercise {Title = "Shoulder Press (strict)", Abbreviation = "ShPs(strict)", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
                new Exercise {Title = "Shoulder Press (DB/KB) (strict)", Abbreviation = "ShPs(DB/KB)(strict)", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
                new Exercise {Title = "Front Langue", Abbreviation = "FL", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
                new Exercise {Title = "Dumbbell snatch", Abbreviation = "DB Snatch", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count,MeasureType.Weight)},
                new Exercise {Title = "Burpee over dumbbell",Abbreviation = "Burpee OvDB", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
                new Exercise {Title = "Snatch deadlift", Abbreviation = "Snatch DL", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
                new Exercise {Title = "Split Jerk", Abbreviation = "Split Jerk", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
                new Exercise {Title = "Push Jerk", Abbreviation = "Push Jerk", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
                new Exercise {Title = "Pull-up (chest to bar)", Abbreviation = "PU(C2B)", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
                new Exercise {Title = "Pull-up (chest to bar) (strict)", Abbreviation = "PU(C2B)(strict)", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
                new Exercise {Title = "Pull-up (strict)", Abbreviation = "PU(strict)", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
                new Exercise {Title = "Hang Power Clean", Abbreviation = "HPC", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
                new Exercise {Title = "Hang Power Snatch", Abbreviation = "HPSN", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
                new Exercise {Title = "Hang Snatch", Abbreviation = "HSN", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
                new Exercise {Title = "Clean and Jerk (DB/KB)", Abbreviation = "C&J(DB/KB)", ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)},
            };
        }

    }
}
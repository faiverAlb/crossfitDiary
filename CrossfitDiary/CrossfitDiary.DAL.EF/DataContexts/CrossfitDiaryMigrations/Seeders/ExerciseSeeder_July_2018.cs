using System.Collections.Generic;
using System.Linq;
using CrossfitDiary.Model;

namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations.Seeders
{
    public static partial class ExerciseSeeder
    {

        internal static List<Exercise> Add_July_2018(CrossfitDiaryDbContext context)
        {
            if (IsExerciseAlreadyExist("Hang Clean: Above Knee", context))
            {
                return new List<Exercise>();
            }

            return new List<Exercise>
            {
                new Exercise
                {
                    Title = "Hang Clean: Above Knee",
                    Abbreviation = "Hang Clean: Above Knee",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Hang Clean: Below Knee",
                    Abbreviation = "Hang Clean: Below Knee",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Clean Pull",
                    Abbreviation = "Clean Pull",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Slam ball drop",
                    Abbreviation = "Slam ball drop",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Seated Shoulder Press",
                    Abbreviation = "Seated Shoulder Press",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Seated Shoulder Press(DB/KB)",
                    Abbreviation = "Seated Shoulder Press(DB/KB)",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Snatch balance",
                    Abbreviation = "Snatch balance",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },

            };
        }
        internal static List<Exercise> Add_July_2018_2(CrossfitDiaryDbContext context)
        {
            if (IsExerciseAlreadyExist("Hang Snatch: Above Knee", context))
            {
                return new List<Exercise>();
            }

            return new List<Exercise>
            {
                new Exercise
                {
                    Title = "Hang Snatch: Above Knee",
                    Abbreviation = "Hang Snatch: Above Knee",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Hang Snatch: Below Knee",
                    Abbreviation = "Hang Snatch: Below Knee",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Back Squat Lunges",
                    Abbreviation = "Back Squat lunges",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Weight, MeasureType.Distance)

                },
                new Exercise
                {
                    Title = "Front Squat Lunges",
                    Abbreviation = "Front Squat Lunges",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Weight, MeasureType.Distance)

                },
            };
        }
      
    }
}
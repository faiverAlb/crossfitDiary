using System.Collections.Generic;
using System.Linq;
using CrossfitDiary.Model;

namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations.Seeders
{
    public static class ExerciseSeeder
    {

        private static ICollection<ExerciseMeasure> GetExerciseMeasures(CrossfitDiaryDbContext context, params MeasureType[] measureTypes)
        {
            var exerciseMeasure = measureTypes.Select(measureType => new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == measureType) }).ToList();
            return exerciseMeasure;
        }

        internal static List<Exercise> GetInitialExercises(CrossfitDiaryDbContext context)
        {
            return new List<Exercise>
            {
                new Exercise
                {
                    Title = "Pull-up",
                    Abbreviation = "PU",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count,MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Push-up",
                    Abbreviation = "PshU",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Air squat",
                    Abbreviation = "AS",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count)
                },
                new Exercise
                {
                    Title = "Sit-up",
                    Abbreviation = "SU",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Deadlift",
                    Abbreviation = "DL",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Handstand Push-Ups",
                    Abbreviation = "HSPU",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count)
                },
                new Exercise
                {
                    Title = "Clean",
                    Abbreviation = "CLN",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Ring Dip",
                    Abbreviation = "Ring Dip",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count)
                },
                new Exercise
                {
                    Title = "Thruster",
                    Abbreviation = "Thruster",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Clean and Jerk",
                    Abbreviation = "C&J",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Run",
                    Abbreviation = "Run",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Distance)
                },
                new Exercise
                {
                    Title = "Swing",
                    Abbreviation = "Swing",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Box Jumps",
                    Abbreviation = "BJ",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count)
                },
                new Exercise
                {
                    Title = "Burpee",
                    Abbreviation = "Burpee",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Bar Muscle-Up",
                    Abbreviation = "BMU",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count)
                },
                new Exercise
                {
                    Title = "Bench Press",
                    Abbreviation = "BP",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Back Squat",
                    Abbreviation = "BS",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Front Squat",
                    Abbreviation = "FS",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Double-Unders",
                    Abbreviation = "DU",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count)
                },
                new Exercise
                {
                    Title = "From Shoulders 2 Overhead",
                    Abbreviation = "FS2OH",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Farmer's Walk",
                    Abbreviation = "FW",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Weight, MeasureType.Distance)
                },
                new Exercise
                {
                    Title = "Handwalk",
                    Abbreviation = "HW",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Distance)
                },
                new Exercise
                {
                    Title = "Jerk",
                    Abbreviation = "Jerk",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count,MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Knees to Elbows",
                    Abbreviation = "KTE",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count)
                },
                new Exercise
                {
                    Title = "Toes to Bar",
                    Abbreviation = "T2B",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count)
                },
                new Exercise
                {
                    Title = "Overhead Squat",
                    Abbreviation = "OHS",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count,MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Power Clean",
                    Abbreviation = "PC",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count,MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Pistols",
                    Abbreviation = "Pistols",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count)
                },
                new Exercise
                {
                    Title = "Plank",
                    Abbreviation = "Plank",
                    ExerciseMeasures = new List<ExerciseMeasure>()
                },
                new Exercise
                {
                    Title = "Push Press",
                    Abbreviation = "PP",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count,MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Power Snatch",
                    Abbreviation = "PSN",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count,MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Rope Climb",
                    Abbreviation = "RC",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count)
                },
                new Exercise
                {
                    Title = "Ring Muscle-Ups",
                    Abbreviation = "RMU",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count)
                },
                new Exercise
                {
                    Title = "Rowing",
                    Abbreviation = "Row",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Distance)
                },
                new Exercise
                {
                    Title = "Sumo Deadlift High Pull",
                    Abbreviation = "SDHP",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count,MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Snatch",
                    Abbreviation = "SN",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count,MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Turkish Get-Up",
                    Abbreviation = "TGU",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count,MeasureType.Weight)
                },
                new Exercise
                {
                    Title = "Walking Lunges",
                    Abbreviation = "WL",
                    ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                },
            };
        }


        internal static List<Exercise> GetAdditionalExercises(CrossfitDiaryDbContext context)
        {
            if (context.Exercises.Any(x => x.Title.ToLower() == "Rowing(calories)".ToLower()))
                return new List<Exercise>();


            return new List<Exercise>
            {
                new Exercise {Title = "Rowing(calories)",Abbreviation = "Row(cal)", ExerciseMeasures = GetExerciseMeasures(context,MeasureType.Calories)},
                new Exercise {Title = "Bike(calories)",Abbreviation = "Bike(cal)",ExerciseMeasures= GetExerciseMeasures(context,MeasureType.Calories)},
                new Exercise {Title = "Bike(distance)",Abbreviation = "Bike(dist)",ExerciseMeasures= GetExerciseMeasures(context,MeasureType.Distance)},
                new Exercise {Title = "Wallwalk",Abbreviation = "Wallwalk",ExerciseMeasures= GetExerciseMeasures(context,MeasureType.Count)},
                new Exercise {Title = "V-ups",Abbreviation = "V-ups",ExerciseMeasures= GetExerciseMeasures(context,MeasureType.Count)},
                new Exercise {Title = "Superman",Abbreviation = "Superman",ExerciseMeasures = GetExerciseMeasures(context,MeasureType.Count)},
                new Exercise {Title = "Superman(hold)",Abbreviation = "Superman(hold)",ExerciseMeasures= new List<ExerciseMeasure>()},
                new Exercise {Title = "Hollow(hold)",Abbreviation = "Hollow(hold)",ExerciseMeasures= new List<ExerciseMeasure>()},
                new Exercise {Title = "Shoulder Press (DB/KB)",Abbreviation = "ShPs(DB/KB)",ExerciseMeasures = GetExerciseMeasures(context,MeasureType.Count,MeasureType.Weight) },
            };
        }

        internal static List<Exercise> GetMoreExercises(CrossfitDiaryDbContext context)
        {
            var exercisesToAdd = new List<Exercise>();
            if (context.Exercises.Any(x => x.Title.ToLower() != "Squat Clean".ToLower()))
            {
                exercisesToAdd.AddRange(new List<Exercise>
                {
                    new Exercise
                    {
                        Title = "Squat Clean",
                        Abbreviation = "Sq.Clean",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                    },
                    new Exercise
                    {
                        Title = "Hang Clean",
                        Abbreviation = "Hang Clean",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                    }
                });
            }

            if (context.Exercises.Any(x => x.Title.ToLower() != "Wallball".ToLower()))
            {
                exercisesToAdd.AddRange(new List<Exercise>
                {
                    new Exercise
                    {
                        Title = "Wallball",
                        Abbreviation = "WB",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                    },
                    new Exercise
                    {
                        Title = "Ring Row",
                        Abbreviation = "Ring Row",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count)
                    },
                    new Exercise
                    {
                        Title = "Kettlebell Thruster",
                        Abbreviation = "KB Thruster",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                    },
                    new Exercise
                    {
                        Title = "Triple unders",
                        Abbreviation = "TU",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count)
                    },
                    new Exercise
                    {
                        Title = "Dip",
                        Abbreviation = "Dip",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                    },
                    new Exercise
                    {
                        Title = "Jumping Jack",
                        Abbreviation = "JJ",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                    },
                    new Exercise
                    {
                        Title = "Kettlebell Snatch",
                        Abbreviation = "KB Snatch",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                    },
                    new Exercise
                    {
                        Title = "Burpee over rower",
                        Abbreviation = "Burpee OvR",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                    },
                    new Exercise
                    {
                        Title = "Burpee over barbell",
                        Abbreviation = "Burpee OvBr",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                    },
                    new Exercise
                    {
                        Title = "Burpee box jump",
                        Abbreviation = "Burpee BJ",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                    },
                    new Exercise
                    {
                        Title = "Burpee box jump over",
                        Abbreviation = "Burpee BJ Ov",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                    },
                    new Exercise
                    {
                        Title = "Lap Run",
                        Abbreviation = "Lap Run",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                    },
                    new Exercise
                    {
                        Title = "Bear Crawl(Lap)",
                        Abbreviation = "Bear Crawl(Lap)",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                    },
                    new Exercise
                    {
                        Title = "Overhead Kettlebell/Dumbbell Lunges(Lap)",
                        Abbreviation = "OH lunges (KB/DB)(Lap)",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                    },
                    new Exercise
                    {
                        Title = "Overhead Kettlebell/Dumbbell Lunges",
                        Abbreviation = "OH lunges (KB/DB)",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Distance, MeasureType.Weight)
                    },
                    new Exercise
                    {
                        Title = "Cluster",
                        Abbreviation = "Cluster",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                    },
                    new Exercise
                    {
                        Title = "Pull-ups (rings)",
                        Abbreviation = "PU(rings)",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                    },
                    new Exercise
                    {
                        Title = "Push-ups (rings)",
                        Abbreviation = "PshU(rings)",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                    },
                    new Exercise
                    {
                        Title = "Hyperextensions (back extensions)",
                        Abbreviation = "B.ext",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                    },
                    new Exercise
                    {
                        Title = "Jumping Lunges",
                        Abbreviation = "JL",
                        ExerciseMeasures = GetExerciseMeasures(context, MeasureType.Count, MeasureType.Weight)
                    },
                });
            }

            return exercisesToAdd;
        }

    }
}
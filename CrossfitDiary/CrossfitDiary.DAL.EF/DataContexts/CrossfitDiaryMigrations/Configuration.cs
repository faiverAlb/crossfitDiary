using System.Collections.Generic;
using CrossfitDiary.Model;

namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContexts\CrossfitDiaryMigrations";
        }

        protected override void Seed(CrossfitDiaryDbContext context)
        {
                GetInitialExerciseMeasureTypes().ForEach(exerciseMeasureType => context.ExerciseMeasureTypes.AddOrUpdate(x => x.MeasureType, exerciseMeasureType));
                context.Commit();
                GetInitialExercises(context).ForEach(x => context.Exercises.AddOrUpdate(y => new {y.Abbreviation}, x));
                context.Commit();
                GetInitialRoutines(context).ForEach(x => context.ComplexRoutines.AddOrUpdate(y => y.Title,x));
                context.Commit();

        }

        
        private static List<ExerciseMeasureType> GetInitialExerciseMeasureTypes()
        {
            return new List<ExerciseMeasureType>
            {
                new ExerciseMeasureType()
                {
                    MeasureType = MeasureType.Distance,
                    Description = "Passed Distance",
                    ShortMeasureDescription = "meters"
                },
                new ExerciseMeasureType()
                {
                    MeasureType = MeasureType.Count,
                    Description = "General count",
                    ShortMeasureDescription = "count"
                },
                new ExerciseMeasureType()
                {
                    MeasureType = MeasureType.Weight,
                    Description = "Weight",
                    ShortMeasureDescription = "lbs"
                },
            };
        }

        private static List<Exercise> GetInitialExercises(CrossfitDiaryDbContext context)
        {
            return new List<Exercise>
            {
                new Exercise {Title = "Pull-up",Abbreviation = "PU",ExerciseMeasures= new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)}}},
                new Exercise {Title = "Push-up",Abbreviation = "PshU",ExerciseMeasures = new List<ExerciseMeasure> { new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Count) } }},
                new Exercise {Title = "Air squat",Abbreviation = "AS",ExerciseMeasures = new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Count) } }},
                new Exercise {Title = "Sit-up",Abbreviation = "SU",ExerciseMeasures = new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Count) } }},
                new Exercise {Title = "Deadlift",Abbreviation = "DL",ExerciseMeasures = new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Count) },new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Weight) } } },
                new Exercise {Title = "Handstand Push-Ups",Abbreviation = "HSPU",ExerciseMeasures = new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Count) } }},
                new Exercise {Title = "Clean",Abbreviation = "CLN",ExerciseMeasures = new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Count) },new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Weight) } }},
                new Exercise {Title = "Ring Dip",Abbreviation = "Ring Dip",ExerciseMeasures = new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Count) } }},
                new Exercise {Title = "Thruster",Abbreviation = "Thruster",ExerciseMeasures = new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Count) },new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Weight) } }},
                new Exercise {Title = "Clean and Jerk",Abbreviation = "C&J",ExerciseMeasures = new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Count) },new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Weight) } }},
                new Exercise {Title = "Run",Abbreviation = "Run",ExerciseMeasures = new List<ExerciseMeasure>() {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Distance) }/*,new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Time) }*/ } },
                new Exercise {Title = "Swing",Abbreviation = "Swing",ExerciseMeasures = new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Count) },new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Weight)}}},
                new Exercise {Title = "Box Jumps",Abbreviation = "BJ",ExerciseMeasures= new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)}}},
                new Exercise {Title = "Burpee",Abbreviation = "Burpee",ExerciseMeasures= new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)}}},
                new Exercise {Title = "Bar Muscle-Up",Abbreviation = "BMU",ExerciseMeasures= new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)}}},
                new Exercise {Title = "Bench Press",Abbreviation = "BP",ExerciseMeasures= new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)},new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Weight)}}},
                new Exercise {Title = "Back Squat",Abbreviation = "BS",ExerciseMeasures= new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)},new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Weight)}}},
                new Exercise {Title = "Front Squat",Abbreviation = "FS",ExerciseMeasures= new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)},new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Weight)}}},
                new Exercise {Title = "Double-Unders",Abbreviation = "DU",ExerciseMeasures= new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)}}},
                new Exercise {Title = "From Shoulders 2 Overhead",Abbreviation = "FS2OH",ExerciseMeasures= new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)},new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Weight)}}},
                new Exercise {Title = "Farmer's Walk",Abbreviation = "FW",ExerciseMeasures= new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Distance)},new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Weight)}}},
                new Exercise {Title = "Handwalk",Abbreviation = "HW",ExerciseMeasures= new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Distance)}}},
                new Exercise {Title = "Jerk",Abbreviation = "Jerk",ExerciseMeasures= new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)},new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Weight)}}},
                new Exercise {Title = "Knees to Elbows",Abbreviation = "KTE",ExerciseMeasures= new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)}}},
                new Exercise {Title = "Toes to Bar",Abbreviation = "T2B",ExerciseMeasures= new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)}}},
                new Exercise {Title = "Overhead Squat",Abbreviation = "OHS",ExerciseMeasures= new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)},new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Weight)}}},
                new Exercise {Title = "Power Clean",Abbreviation = "PC",ExerciseMeasures= new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)},new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Weight)}}},
                new Exercise {Title = "Pistols",Abbreviation = "Pistols",ExerciseMeasures= new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)}}},
                new Exercise {Title = "Plank",Abbreviation = "Plank",ExerciseMeasures= new List<ExerciseMeasure>()},
                new Exercise {Title = "Push Press",Abbreviation = "PP",ExerciseMeasures= new List<ExerciseMeasure>(){new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)},{new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Weight)}}}},
                new Exercise {Title = "Power Snatch",Abbreviation = "PSN",ExerciseMeasures= new List<ExerciseMeasure>(){new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)},{new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Weight)}}}},
                new Exercise {Title = "Rope Climb",Abbreviation = "RC",ExerciseMeasures= new List<ExerciseMeasure>(){new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)}}},
                new Exercise {Title = "Ring Muscle-Ups",Abbreviation = "RMU",ExerciseMeasures= new List<ExerciseMeasure>(){new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)}}},
                new Exercise {Title = "Rowing",Abbreviation = "Row",ExerciseMeasures= new List<ExerciseMeasure>(){new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Distance)}}},
                new Exercise {Title = "Sumo Deadlift High Pull",Abbreviation = "SDHP",ExerciseMeasures= new List<ExerciseMeasure>(){new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)},new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Weight)}}},
                new Exercise {Title = "Snatch",Abbreviation = "SN",ExerciseMeasures= new List<ExerciseMeasure>(){new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)},new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Weight)}}},
                new Exercise {Title = "Turkish Get-Up",Abbreviation = "TGU",ExerciseMeasures= new List<ExerciseMeasure>(){new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)},new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Weight)}}},
                new Exercise {Title = "Walking Lunges",Abbreviation = "WL",ExerciseMeasures= new List<ExerciseMeasure>(){new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)},new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Distance)}}},

            };
        }


        private static List<RoutineComplex> GetInitialRoutines(CrossfitDiaryDbContext context)
        {
            return new List<RoutineComplex>()
              {
                  new RoutineComplex
                  {
                      RoutineSimple = new List<RoutineSimple>
                        {
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Pull-up"), Count = 100},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Push-up"), Count = 100},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Sit-up"), Count = 100},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Air squat"), Count = 100}
                        }
                        , Title = "Angie"
                        , ComplexType = RoutineComplexType.ForTime
                  }
                  ,new RoutineComplex
                  {
                      RoutineSimple = new List<RoutineSimple>
                        {
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Pull-up"), Count = 20},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Push-up"), Count = 30},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Sit-up"), Count = 40},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Air squat"), Count = 50}
                        }
                        , Title = "Barbara"
                        , ComplexType = RoutineComplexType.ForTime
                        , RoundCount = 5
                  }
                  ,new RoutineComplex
                  {
                      RoutineSimple = new List<RoutineSimple>
                        {
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Pull-up"), Count = 5},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Push-up"), Count = 10},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Air squat"), Count = 15},
                        }
                        , Title = "Chelsea"
                        , ComplexType = RoutineComplexType.EMOM
                        , TimeToWork = TimeSpan.FromMinutes(30)
                  }
                  ,new RoutineComplex
                  {
                      RoutineSimple = new List<RoutineSimple>
                        {
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Pull-up"), Count = 5},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Push-up"), Count = 10},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Air squat"), Count = 15}
                        }
                        , Title = "Cindy"
                        , TimeToWork = TimeSpan.FromMinutes(20)
                        , ComplexType = RoutineComplexType.AMRAP
                  }
                  ,new RoutineComplex
                  {
                      RoutineSimple = new List<RoutineSimple>
                        {
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Deadlift"), Weight = 225, Count = 21},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Handstand Push-Ups"), Count = 21},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Deadlift"), Weight = 225, Count = 15},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Handstand Push-Ups"), Count = 15},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Deadlift"), Weight = 225, Count = 9},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Handstand Push-Ups"), Count = 9},
                        }
                        , Title = "Diane"
                        , ComplexType = RoutineComplexType.ForTime
                  }
                  ,new RoutineComplex
                  {
                      RoutineSimple = new List<RoutineSimple>
                        {
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Clean"), Weight = 135, Count = 21},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Ring Dip"), Count = 21},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Clean"), Weight = 135, Count = 15},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Ring Dip"), Count = 15},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Clean"), Weight = 135, Count = 9},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Ring Dip"), Count = 9}
                        }
                        , Title = "Elizabeth"
                        , ComplexType = RoutineComplexType.ForTime
                  }
                  ,new RoutineComplex
                  {
                      RoutineSimple = new List<RoutineSimple>
                        {
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Clean"), Weight = 95, Count = 21},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Pull-up"), Count = 21},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Clean"), Weight = 95, Count = 15},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Pull-up"), Count = 15},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Clean"), Weight = 95, Count = 9},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Pull-up"), Count = 9},
                        }
                        , Title = "Fran"
                        , ComplexType = RoutineComplexType.ForTime
                  }
                  ,new RoutineComplex
                  {
                      RoutineSimple = new List<RoutineSimple>
                        {
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Clean and Jerk"), Weight = 135, Count = 30},
                        }
                        , Title = "Grace"
                        , ComplexType = RoutineComplexType.ForTime
                  }
                  ,new RoutineComplex
                  {
                      RoutineSimple = new List<RoutineSimple>
                        {
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Run"), Distance = 400},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Swing"), Weight = 54, Count = 21},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Pull-up"), Count = 12},
                        }
                        , Title = "Helen"
                        , ComplexType = RoutineComplexType.ForTime
                        , RoundCount = 3
                  }
              };
        }
    }
}

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

        protected override void Seed(CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryDbContext context)
        {
            if (!context.ExerciseMeasureTypes.Any(x => x.MeasureType == MeasureType.Count))
            {
                GetInitialExerciseMeasureTypes()
                    .ForEach(exerciseMeasureType => context.ExerciseMeasureTypes.Add(exerciseMeasureType));
                context.Commit();
                GetInitialExercises(context).ForEach(x => context.Exercises.Add(x));
                context.Commit();
                GetInitialRoutines(context).ForEach(x => context.ComplexRoutines.Add(x));
                context.Commit();
//                GetInitialCrossfitters().ForEach(x => context.Crossfitters.Add(x));
//                context.Commit();
//                GetInitialCrossfitterWorkout(context).ForEach(x => context.CrossfitterWorkouts.Add(x));
//                context.Commit();
            }
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
                    MeasureType = MeasureType.Time,
                    Description = "Time",
                    ShortMeasureDescription = "min."
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
                new Exercise {Title = "Pull-up",ExerciseMeasures= new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single( x => x.MeasureType == MeasureType.Count)}}},
                new Exercise {Title = "Push-up",ExerciseMeasures = new List<ExerciseMeasure> { new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Count) } }},
                new Exercise {Title = "Air squat",ExerciseMeasures = new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Count) } }},
                new Exercise {Title = "Sit-up",ExerciseMeasures = new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Count) } }},
                new Exercise {Title = "Deadlift",ExerciseMeasures = new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Count) },new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Weight) } } },
                new Exercise {Title = "HSPU",ExerciseMeasures = new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Count) } }},
                new Exercise {Title = "Clean",ExerciseMeasures = new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Count) },new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Weight) } }},
                new Exercise {Title = "Ring Dip",ExerciseMeasures = new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Count) } }},
                new Exercise {Title = "Thruster",ExerciseMeasures = new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Count) },new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Weight) } }},
                new Exercise {Title = "Clean and Jerk",ExerciseMeasures = new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Count) },new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Weight) } }},
                new Exercise {Title = "Run",ExerciseMeasures = new List<ExerciseMeasure>() {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Distance) },new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Time) } } },
                new Exercise {Title = "Kettlebell swing",ExerciseMeasures = new List<ExerciseMeasure> {new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Count) },new ExerciseMeasure() { ExerciseMeasureType = context.ExerciseMeasureTypes.Single(x => x.MeasureType == MeasureType.Weight)}}},

            };
        }

//        private static List<Crossfitter> GetInitialCrossfitters()
//        {
//            return new List<Crossfitter>()
//            {
//                new Crossfitter()
//                {
//                    FirstName = "Pukie",
//                    LastName = "Pukie"
//                }
//            };
//        }

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
                        , ComplexType = RoutineComplexType.RoundsForTime
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
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "HSPU"), Count = 21},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Deadlift"), Weight = 225, Count = 15},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "HSPU"), Count = 15},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Deadlift"), Weight = 225, Count = 9},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "HSPU"), Count = 9},
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
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Kettlebell swing"), Weight = 54, Count = 21},
                            new RoutineSimple() {Exercise = context.Exercises.Single(x => x.Title == "Pull-up"), Count = 12},
                        }
                        , Title = "Helen"
                        , ComplexType = RoutineComplexType.RoundsForTime
                        , RoundCount = 3
                  }
              };
        }

//        private static List<CrossfitterWorkout> GetInitialCrossfitterWorkout(CrossfitDiaryDbContext context)
//        {
//
//            return new List<CrossfitterWorkout>()
//            {
//                new CrossfitterWorkout
//                {
//                    RoundsFinished = 30,
//                    PartialRepsFinished = 2,
//                    RoutineComplex = context.ComplexRoutines.Single(x => x.Title == "Cindy"),
//                    Crossfitter = context.Crossfitters.Single(x => x.FirstName == "Pukie")
//                },
//
//            };
//        }
    }
}

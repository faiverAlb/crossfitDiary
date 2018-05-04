using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;
using CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations.Seeders;
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
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());
        }

        internal class CustomSqlServerMigrationSqlGenerator : SqlServerMigrationSqlGenerator
        {
            protected override void Generate(AddColumnOperation addColumnOperation)
            {
                SetCreatedUtcColumn(addColumnOperation.Column);

                base.Generate(addColumnOperation);
            }

            protected override void Generate(CreateTableOperation createTableOperation)
            {
                SetCreatedUtcColumn(createTableOperation.Columns);

                base.Generate(createTableOperation);
            }

            private static void SetCreatedUtcColumn(IEnumerable<ColumnModel> columns)
            {
                foreach (var columnModel in columns)
                {
                    SetCreatedUtcColumn(columnModel);
                }
            }

            private static void SetCreatedUtcColumn(PropertyModel column)
            {
                if (column.Name == "CreatedUtc")
                {
                    column.DefaultValueSql = "GETUTCDATE()";
                }
            }
        }

        protected override void Seed(CrossfitDiaryDbContext context)
        {
            GetInitialExerciseMeasureTypes().ForEach(exerciseMeasureType => context.ExerciseMeasureTypes.AddOrUpdate(x => x.MeasureType, exerciseMeasureType));
            context.Commit();
            ExerciseSeeder.GetInitialExercises(context).ForEach(x => context.Exercises.AddOrUpdate(y => new {y.Abbreviation}, x));
            context.Commit();
            GetInitialRoutines(context).ForEach(x => context.ComplexRoutines.AddOrUpdate(y => y.Title,x));
            context.Commit();

            ExerciseSeeder.GetAdditionalExercises(context).ForEach(x => context.Exercises.AddOrUpdate(y => new { y.Abbreviation }, x));
            context.Commit();

            ExerciseSeeder.GetMoreExercises(context).ForEach(x => context.Exercises.AddOrUpdate(y => new { y.Abbreviation }, x));
            context.Commit();

            ExerciseSeeder.AddSeeds_February_2018(context).ForEach(x => context.Exercises.AddOrUpdate(y => new { y.Abbreviation }, x));
            context.Commit();

            ExerciseSeeder.AddSeeds_May_2018(context).ForEach(x => context.Exercises.AddOrUpdate(y => new { y.Abbreviation }, x));
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
                    ShortMeasureDescription = "kgs"
                },
                new ExerciseMeasureType()
                {
                    MeasureType = MeasureType.Calories,
                    Description = "Calories",
                    ShortMeasureDescription = "cal"
                }
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
                        , RoundCount = 1
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

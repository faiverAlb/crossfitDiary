using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using CrossfitDiary.DAL.EF.Configuration;
using CrossfitDiary.Model;

namespace CrossfitDiary.DAL.EF.DataContexts
{
    public class CrossfitDiaryDbContext : DbContext
    {
        public CrossfitDiaryDbContext() : base("CrossfitDiaryEntities") { }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseMeasure> ExerciseMeasures { get; set; }
        public DbSet<RoutineSimple> SimpleRoutines { get; set; }
        public DbSet<RoutineComplex> ComplexRoutines { get; set; }
        public DbSet<ExerciseMeasureType> ExerciseMeasureTypes { get; set; }

        public DbSet<Crossfitter> Crossfitters { get; set; }

        public DbSet<CrossfitterWorkout> CrossfitterWorkouts { get; set; }


        public virtual void Commit()
        {
            try
            {
                SaveChanges();

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ExerciseConfiguration());
            modelBuilder.Configurations.Add(new RoutineSimpleConfiguration());
            modelBuilder.Configurations.Add(new RoutineComplexConfiguration());
            modelBuilder.Configurations.Add(new ExerciseMeasureConfiguration());
            modelBuilder.Configurations.Add(new CrossfitterConfiguration());
            modelBuilder.Configurations.Add(new CrossfitterWorkoutConfiguration());
            modelBuilder.Configurations.Add(new ExerciseMeasureTypeConfiguration());
        }
    }
}
﻿using CrossfitDiaryCore.DAL.EF.Exercises;
using CrossfitDiaryCore.Model;
using Microsoft.EntityFrameworkCore;

namespace CrossfitDiaryCore.DAL.EF
{
    public class WorkouterContext: IdentityDbContext
    {
        public WorkouterContext()
        {

        }
        public WorkouterContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseMeasure> ExerciseMeasures { get; set; }
        public DbSet<RoutineSimple> SimpleRoutines { get; set; }
        public DbSet<RoutineComplex> ComplexRoutines { get; set; }
        public DbSet<ExerciseMeasureType> ExerciseMeasureTypes { get; set; }

        public DbSet<CrossfitterWorkout> CrossfitterWorkouts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ExerciseConfiguration());
        }
    }
}
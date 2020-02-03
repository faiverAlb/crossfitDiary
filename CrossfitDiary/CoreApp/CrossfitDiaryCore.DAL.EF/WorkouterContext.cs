using System.Collections.Generic;
using CrossfitDiaryCore.DAL.EF.CrossfitterWorkouts;
using CrossfitDiaryCore.DAL.EF.ExerciseMeasures;
using CrossfitDiaryCore.DAL.EF.Exercises;
using CrossfitDiaryCore.DAL.EF.PlanningHistory;
using CrossfitDiaryCore.DAL.EF.RoutinesComplex;
using CrossfitDiaryCore.DAL.EF.RoutinesSimple;
using CrossfitDiaryCore.Model;
using Microsoft.EntityFrameworkCore;

namespace CrossfitDiaryCore.DAL.EF
{
    public class WorkouterContext : IdentityDbContext
    {
        public WorkouterContext()
        {
        }

        public WorkouterContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseMeasure> ExerciseMeasures { get; set; }
        public DbSet<RoutineSimple> SimpleRoutines { get; set; }
        public DbSet<RoutineComplex> ComplexRoutines { get; set; }
        public DbSet<Model.PlanningHistory> PlanningHistories { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }

        public virtual DbSet<CrossfitterWorkout> CrossfitterWorkouts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ExerciseConfiguration());
            builder.ApplyConfiguration(new RoutineSimpleConfiguration());
            builder.ApplyConfiguration(new RoutineComplexConfiguration());
            builder.ApplyConfiguration(new ExerciseMeasureConfiguration());
            builder.ApplyConfiguration(new CrossfitterWorkoutConfiguration());
            builder.ApplyConfiguration(new PlanningHistoryConfiguration());

            SeedData(builder);
        }


        private void SeedData(ModelBuilder builder)
        {
            int fromExerciseId = 130;
            int fromMeasuresId = 311;
            
            SeedDataWeightliftingExercise(new Exercise { Id = fromExerciseId++, Abbreviation = "HSC", Title = "Hang Squat Clean" }, builder, ref fromMeasuresId);
            SeedDataWeightliftingExercise(new Exercise { Id = fromExerciseId++, Abbreviation = "HCP", Title = "Hang Clean Pull" }, builder, ref fromMeasuresId);
            SeedDataWeightliftingExercise(new Exercise { Id = fromExerciseId++, Abbreviation = "DDL", Title = "Deficit Deadlift" }, builder, ref fromMeasuresId);
            SeedDataTimeCountExercise(new Exercise { Id = fromExerciseId++, Abbreviation = "PSBH", Title = "Pistol squat bottom hold" }, builder, ref fromMeasuresId);
            SeedDataWeightliftingExercise(new Exercise { Id = fromExerciseId++, Abbreviation = "Pegboard", Title = "Pegboard" }, builder, ref fromMeasuresId);
            SeedDataWeightliftingExercise(new Exercise { Id = fromExerciseId++, Abbreviation = "Ring PshU", Title = "Ring Push-Up" }, builder, ref fromMeasuresId);
            SeedDataWeightliftingExercise(new Exercise { Id = fromExerciseId++, Abbreviation = "Burpee D-ball over box", Title = "Burpee D-ball over box" }, builder, ref fromMeasuresId);
            SeedDataTimeCountExercise(new Exercise { Id = fromExerciseId++, Abbreviation = "Hip extension hold (GHD)", Title = "Hip extension hold (GHD)" }, builder, ref fromMeasuresId);
            SeedDataWeightliftingExercise(new Exercise { Id = fromExerciseId++, Abbreviation = "Glute bridge", Title = "Glute bridge" }, builder, ref fromMeasuresId);
            SeedCountOnlyExercise(new Exercise { Id = fromExerciseId++, Abbreviation = "Lat flys", Title = "Lat flys" }, builder, ref fromMeasuresId);
            SeedDataTimeCountExercise(new Exercise { Id = fromExerciseId++, Abbreviation = "C2B hold", Title = "Chest to bar hold" }, builder, ref fromMeasuresId);
            SeedDataTimeCountExercise(new Exercise { Id = fromExerciseId++, Abbreviation = "COvB hold", Title = "Chin over bar hold" }, builder, ref fromMeasuresId);
            SeedDataWeightliftingExercise(new Exercise { Id = fromExerciseId++, Abbreviation = "Bottom up OHS", Title = "Bottom up overhead squat" }, builder, ref fromMeasuresId);

            // Seed measures for Plank  - add time 
            SeedDataTimeMeasuresForExercise(38, builder, ref fromMeasuresId);
        }

        private void SeedDataTimeMeasuresForExercise(int exerciseId, ModelBuilder builder, ref int fromMeasuresId)
        {
            int id = fromMeasuresId;
            builder.Entity<ExerciseMeasure>(b =>
            {
                var exerciseMeasure = new ExerciseMeasure() { Id = id, ExerciseMeasureTypeId = MeasureType.Time, ExerciseId = exerciseId };
                b.HasData(exerciseMeasure);
            });

            fromMeasuresId++;

        }

        private void SeedDataTimeCountExercise(Exercise exercise, ModelBuilder builder,ref int fromMeasuresId)
        {
            var exerciseMeasures = new List<ExerciseMeasure>
            {
                new ExerciseMeasure() {Id = fromMeasuresId++, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = exercise.Id},
                new ExerciseMeasure() {Id = fromMeasuresId++, ExerciseMeasureTypeId = MeasureType.Time, ExerciseId = exercise.Id},
            };
            SeedData(exercise, builder, exerciseMeasures);
        }
        private void SeedCountOnlyExercise(Exercise exercise, ModelBuilder builder,ref int fromMeasuresId)
        {
            var exerciseMeasures = new List<ExerciseMeasure>
            {
                new ExerciseMeasure() {Id = fromMeasuresId++, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = exercise.Id},
            };
            SeedData(exercise, builder, exerciseMeasures);
        }

        private void SeedDataWeightliftingExercise(Exercise exercise, ModelBuilder builder, ref int fromId)
        {
            List<ExerciseMeasure> weightLiftingMeasures = new List<ExerciseMeasure>
            {
                new ExerciseMeasure {Id = fromId++, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = exercise.Id},
                new ExerciseMeasure {Id = fromId++, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = exercise.Id},
                new ExerciseMeasure {Id = fromId++, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = exercise.Id}
            };
            SeedData(exercise, builder, weightLiftingMeasures);
        }

        private void SeedData(Exercise exercise, ModelBuilder builder, List<ExerciseMeasure> measures)
        {
            builder.Entity<Exercise>(b => { b.HasData(exercise); });
            builder.Entity<ExerciseMeasure>(b => { b.HasData(measures); });
        }
    }
}
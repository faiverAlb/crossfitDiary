using CrossfitDiaryCore.DAL.EF.CrossfitterWorkouts;
using CrossfitDiaryCore.DAL.EF.ExerciseMeasures;
using CrossfitDiaryCore.DAL.EF.Exercises;
using CrossfitDiaryCore.DAL.EF.RoutinesComplex;
using CrossfitDiaryCore.DAL.EF.RoutinesSimple;
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

        public virtual DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseMeasure> ExerciseMeasures { get; set; }
        public DbSet<RoutineSimple> SimpleRoutines { get; set; }
        public DbSet<RoutineComplex> ComplexRoutines { get; set; }

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


        }
    }
}
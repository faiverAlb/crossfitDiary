using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using CrossfitDiary.DAL.EF.Configuration;
using CrossfitDiary.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CrossfitDiary.DAL.EF.DataContexts
{
    public class CrossfitDiaryDbContext : IdentityDbContext
    {
//        public CrossfitDiaryDbContext() : base("CrossfitDiaryEntities")
//        {
//            Database.SetInitializer<CrossfitDiaryDbContext>(null);
//
//        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseMeasure> ExerciseMeasures { get; set; }
        public DbSet<RoutineSimple> SimpleRoutines { get; set; }
        public DbSet<RoutineComplex> ComplexRoutines { get; set; }
        public DbSet<ExerciseMeasureType> ExerciseMeasureTypes { get; set; }

//        public DbSet<ApplicationUser> Crossfitters { get; set; }

        public DbSet<CrossfitterWorkout> CrossfitterWorkouts { get; set; }


        public virtual void Commit()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ApplicationUser>().HasMany(x => x.CrossfitterWorkout);
            modelBuilder.Configurations.Add(new ExerciseConfiguration());
            modelBuilder.Configurations.Add(new RoutineSimpleConfiguration());
            modelBuilder.Configurations.Add(new RoutineComplexConfiguration());
            modelBuilder.Configurations.Add(new ExerciseMeasureConfiguration());
//            modelBuilder.Configurations.Add(new CrossfitterConfiguration());
            modelBuilder.Configurations.Add(new CrossfitterWorkoutConfiguration());
            modelBuilder.Configurations.Add(new ExerciseMeasureTypeConfiguration());

//            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId }).ToTable("AspNetUserLogins");
//            modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id).ToTable("AspNetRoles");
//            modelBuilder.Entity<IdentityUserRole>().HasKey(ur => new { ur.UserId, ur.RoleId }).ToTable("AspNetUserRoles");
//            modelBuilder.Entity<IdentityUserClaim>().HasKey(claim => claim.UserId).ToTable("AspNetUserClaims");
//            modelBuilder.Entity<IdentityUser>().HasKey(user => user.Id).ToTable("AspNetUsers");
        }
    }
}
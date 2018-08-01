using CrossfitDiaryCore.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrossfitDiaryCore.DAL.EF
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityDbContext()
        {

        }
        public IdentityDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                   .HasMany(x => x.CrossfitterWorkout)
                   .WithOne(x => x.Crossfitter)
                   .IsRequired();
            builder.Entity<ApplicationUser>().HasMany(x => x.RoutineComplexCollection);
        }
    }
}
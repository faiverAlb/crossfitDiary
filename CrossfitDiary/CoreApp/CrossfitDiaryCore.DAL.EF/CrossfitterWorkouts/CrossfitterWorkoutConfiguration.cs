using CrossfitDiaryCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrossfitDiaryCore.DAL.EF.CrossfitterWorkouts
{
    public class CrossfitterWorkoutConfiguration : IEntityTypeConfiguration<CrossfitterWorkout>
    {
        public void Configure(EntityTypeBuilder<CrossfitterWorkout> builder)
        {
            builder.ToTable("CrossfitterWorkout");
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.RoutineComplexId).IsRequired();

            builder.HasOne(x => x.Crossfitter).WithMany(x => x.CrossfitterWorkout).HasForeignKey("Crossfitter_Id");
            builder.HasOne(x => x.RoutineComplex);

            builder.Property(x => x.Date).IsRequired();
        }
    }
}
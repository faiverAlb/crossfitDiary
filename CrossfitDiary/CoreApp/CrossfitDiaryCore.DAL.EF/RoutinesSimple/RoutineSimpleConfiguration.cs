using CrossfitDiaryCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrossfitDiaryCore.DAL.EF.RoutinesSimple
{
    public class RoutineSimpleConfiguration : IEntityTypeConfiguration<RoutineSimple>
    {
        public void Configure(EntityTypeBuilder<RoutineSimple> builder)
        {
            builder.ToTable("RoutineSimple");

            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.ExerciseId).IsRequired();
            builder.HasOne(x => x.Exercise);
        }
    }
}
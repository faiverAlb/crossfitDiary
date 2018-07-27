using CrossfitDiaryCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrossfitDiaryCore.DAL.EF.ExerciseMeasures
{
    public class ExerciseMeasureConfiguration : IEntityTypeConfiguration<ExerciseMeasure>
    {
        public void Configure(EntityTypeBuilder<ExerciseMeasure> builder)
        {
            builder.ToTable("ExerciseMeasure");
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.ExerciseMeasureTypeId).IsRequired();
        }
    }
}
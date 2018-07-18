using CrossfitDiaryCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrossfitDiaryCore.DAL.EF.Configurations
{
    public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public ExerciseConfiguration()
        {
//            ToTable("Exercise");
//            Property(x => x.Id).IsRequired();
//            Property(x => x.Title).IsRequired();
//            Property(x => x.Abbreviation).IsRequired();
//            HasMany(x => x.ExerciseMeasures).WithRequired(x => x.Exercise);
        }

        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.ToTable("Exercise");

            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Abbreviation).IsRequired();
            builder.HasMany(x => x.ExerciseMeasures).WithOne(x => x.Exercise);
        }
    }
}
using CrossfitDiaryCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrossfitDiaryCore.DAL.EF.RoutinesComplex
{
    public class RoutineComplexConfiguration : IEntityTypeConfiguration<RoutineComplex>
    {
        public void Configure(EntityTypeBuilder<RoutineComplex> builder)
        {
            builder.ToTable("RoutineComplex");
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Title);
            builder.Property(x => x.ComplexType).IsRequired();

            builder.HasMany(x => x.RoutineSimple).WithOne(x => x.RoutineComplex);
            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId);
        }
    }
}
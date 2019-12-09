using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrossfitDiaryCore.DAL.EF.PlanningHistory
{
    public class PlanningHistoryConfiguration : IEntityTypeConfiguration<Model.PlanningHistory>
    {
        public void Configure(EntityTypeBuilder<Model.PlanningHistory> builder)
        {
            builder.ToTable("PlanningHistory");
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.RoutineComplexId).IsRequired();
            builder.Property(x => x.PlanningDate).IsRequired();
            builder.Property(x => x.CreatedUtc).HasDefaultValueSql("getutcdate()");
            builder.HasOne(x => x.Crossfitter).WithMany(x => x.PlanningHistoryCollection);

        }
    }
}

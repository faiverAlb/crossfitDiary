using System.Data.Entity.ModelConfiguration;
using CrossfitDiary.Model;

namespace CrossfitDiary.DAL.EF.Configuration
{
    public class CrossfitterConfiguration : EntityTypeConfiguration<Crossfitter>
    {
        public CrossfitterConfiguration()
        {
            ToTable("Crossfitter");
            Property(x => x.Id).IsRequired();

            Property(x => x.FirstName).IsRequired();
            Property(x => x.LastName).IsRequired();
        }
    }
}
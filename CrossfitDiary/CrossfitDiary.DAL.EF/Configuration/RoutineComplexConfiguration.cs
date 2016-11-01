using System.Data.Entity.ModelConfiguration;
using CrossfitDiary.Model;

namespace CrossfitDiary.DAL.EF.Configuration
{
    public class RoutineComplexConfiguration : EntityTypeConfiguration<RoutineComplex>
    {
        public RoutineComplexConfiguration()
        {
            ToTable("RoutineComplex");
            Property(x => x.Id).IsRequired();
            Property(x => x.Title).IsRequired();
            Property(x => x.ComplexType).IsRequired();
        }
    }
}
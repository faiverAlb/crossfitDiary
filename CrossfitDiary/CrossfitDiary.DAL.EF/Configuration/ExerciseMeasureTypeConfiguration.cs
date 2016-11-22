using System.Data.Entity.ModelConfiguration;
using CrossfitDiary.Model;

namespace CrossfitDiary.DAL.EF.Configuration
{
    public class ExerciseMeasureTypeConfiguration : EntityTypeConfiguration<ExerciseMeasureType>
    {
        public ExerciseMeasureTypeConfiguration()
        {
            ToTable("ExerciseMeasureType");
            Property(x => x.Id).IsRequired();
            Property(x => x.Description).IsRequired();
            Property(x => x.MeasureType).IsRequired();
            Property(x => x.ShortMeasureDescription).IsRequired();
        }

        
    }
}
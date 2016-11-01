using System.Data.Entity.ModelConfiguration;
using CrossfitDiary.Model;

namespace CrossfitDiary.DAL.EF.Configuration
{
    public class ExerciseMeasureConfiguration : EntityTypeConfiguration<ExerciseMeasure>
    {
        public ExerciseMeasureConfiguration()
        {
            ToTable("ExerciseMeasure");
            Property(x => x.Id).IsRequired();
            Property(x => x.ExerciseMeasureTypeId).IsRequired();
//            HasRequired(x => x.ExerciseMeasureType);
        }
    }
}
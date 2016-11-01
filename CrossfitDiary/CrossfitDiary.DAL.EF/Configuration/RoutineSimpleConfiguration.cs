using System.Data.Entity.ModelConfiguration;
using CrossfitDiary.Model;

namespace CrossfitDiary.DAL.EF.Configuration
{
    public class RoutineSimpleConfiguration : EntityTypeConfiguration<RoutineSimple>
    {
        public RoutineSimpleConfiguration()
        {
            ToTable("RoutineSimple");
            Property(x => x.Id).IsRequired();
            Property(x => x.ExerciseId).IsRequired();
        }
    }
}
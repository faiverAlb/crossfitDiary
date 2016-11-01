using System.Data.Entity.ModelConfiguration;
using CrossfitDiary.Model;

namespace CrossfitDiary.DAL.EF.Configuration
{
    public class ExerciseConfiguration : EntityTypeConfiguration<Exercise>
    {
        public ExerciseConfiguration()
        {
            ToTable("Exercise");
            Property(x => x.Id).IsRequired();
            Property(x => x.Title).IsRequired();
        }
    }
}
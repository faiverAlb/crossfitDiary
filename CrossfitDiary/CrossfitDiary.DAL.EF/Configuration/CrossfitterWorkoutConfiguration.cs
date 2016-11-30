using System.Data.Entity.ModelConfiguration;
using CrossfitDiary.Model;

namespace CrossfitDiary.DAL.EF.Configuration
{
    public class CrossfitterWorkoutConfiguration : EntityTypeConfiguration<CrossfitterWorkout>
    {
        public CrossfitterWorkoutConfiguration()
        {
            ToTable("CrossfitterWorkout");
            Property(x => x.Id).IsRequired();
            Property(x => x.RoutineComplexId).IsRequired();
//            Property(x => x.CrossfitterId).IsRequired();
            HasRequired(x => x.Crossfitter);

            Property(x => x.Date).IsRequired();
        }
    }
}
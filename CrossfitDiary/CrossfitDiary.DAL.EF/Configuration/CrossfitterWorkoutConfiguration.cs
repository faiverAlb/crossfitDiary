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
            HasRequired(x => x.Crossfitter);
            HasRequired(x => x.RoutineComplex);

            Property(x => x.Date).IsRequired();
        }
    }
}
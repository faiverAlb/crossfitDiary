using System.Collections.Generic;
using System.Linq;
using CrossfitDiaryCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrossfitDiaryCore.DAL.EF.Exercises
{
    public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.ToTable("Exercise");

            builder.Property(x => x.Id).IsRequired().UseIdentityColumn();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Abbreviation).IsRequired();
            builder.HasMany(x => x.ExerciseMeasures).WithOne(x => x.Exercise);
            builder.Property(x => x.CreatedUtc).HasDefaultValueSql("getutcdate()");

            // SeedData(builder);
        }

        private List<ExerciseMeasure> WeightLiftingMeasures(Exercise exercise)
        {
            return new List<ExerciseMeasure>
            {
                new ExerciseMeasure() {ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = exercise.Id, Exercise = exercise},
                new ExerciseMeasure() {ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = exercise.Id, Exercise = exercise},
                new ExerciseMeasure() {ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = exercise.Id, Exercise = exercise},
            };
        }

        // private void SeedData(EntityTypeBuilder<Exercise> builder)
        // {
        //     // builder.OwnsMany(x => x.ExerciseMeasures).HasData(WeightLiftingMeasures(131));
        //     // builder.OwnsMany(x => x.ExerciseMeasures).HasData(WeightLiftingMeasures(132));
        //
        //     Exercise exercise = new Exercise()
        //     {
        //         Id = 130,
        //         Abbreviation = "HSC",
        //         Title = "Hang Squat Clean",
        //         // ExerciseMeasures = WeightLiftingMeasures(130)
        //     };
        //     exercise.ExerciseMeasures = WeightLiftingMeasures(exercise);
        //     builder.HasData(exercise);
        //     // builder.OwnsMany(x => x.ExerciseMeasures).HasData(WeightLiftingMeasures(exercise));
        //
        //     //
        //     // builder.HasData(new Exercise()
        //     // {
        //     //     Id = 131,
        //     //     Abbreviation = "HCP",
        //     //     Title = "Hang Clean Pull",
        //     //     ExerciseMeasures = WeightLiftingMeasures(131)
        //     // });
        //     //
        //     // builder.HasData(new Exercise()
        //     // {
        //     //     Id = 132,
        //     //     Abbreviation = "DDL",
        //     //     Title = "Deadlift",
        //     //     ExerciseMeasures = WeightLiftingMeasures(132)
        //     // });
        // }
    }
}
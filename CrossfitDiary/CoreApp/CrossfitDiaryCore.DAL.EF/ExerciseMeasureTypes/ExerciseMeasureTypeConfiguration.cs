//using CrossfitDiaryCore.Model;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace CrossfitDiaryCore.DAL.EF.ExerciseMeasureTypes
//{
//    public class ExerciseMeasureTypeConfiguration : IEntityTypeConfiguration<ExerciseMeasureType>
//    {
//        public void Configure(EntityTypeBuilder<ExerciseMeasureType> builder)
//        {
//            builder.ToTable("ExerciseMeasureType");
//            builder.Property(x => x.Id).IsRequired();
//            builder.Property(x => x.Description).IsRequired();
//            builder.Property(x => x.MeasureType).IsRequired();
//            builder.Property(x => x.ShortMeasureDescription).IsRequired();
//            builder.Property(x => x.CreatedUtc).HasDefaultValueSql("getutcdate()");
//        }
//    }
//}
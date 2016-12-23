namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMeasureValueType_FromInt_ToDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RoutineSimple", "Count", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.RoutineSimple", "Distance", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.RoutineSimple", "Weight", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RoutineSimple", "Weight", c => c.Int());
            AlterColumn("dbo.RoutineSimple", "Distance", c => c.Int());
            AlterColumn("dbo.RoutineSimple", "Count", c => c.Int());
        }
    }
}

namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeasureType_Add_Column_ShortDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExerciseMeasureType", "ShortMeasureDescription", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExerciseMeasureType", "ShortMeasureDescription");
        }
    }
}

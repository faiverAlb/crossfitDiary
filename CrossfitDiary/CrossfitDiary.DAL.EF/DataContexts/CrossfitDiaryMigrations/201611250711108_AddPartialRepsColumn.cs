namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPartialRepsColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CrossfitterWorkout", "PartialRepsFinished", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CrossfitterWorkout", "PartialRepsFinished");
        }
    }
}

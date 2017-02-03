namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsPlanned : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CrossfitterWorkout", "IsPlanned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CrossfitterWorkout", "IsPlanned");
        }
    }
}

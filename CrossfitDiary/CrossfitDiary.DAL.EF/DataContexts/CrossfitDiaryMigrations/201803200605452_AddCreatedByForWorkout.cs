namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedByForWorkout : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoutineComplex", "CreatedBy_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.RoutineComplex", "CreatedBy_Id");
            AddForeignKey("dbo.RoutineComplex", "CreatedBy_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoutineComplex", "CreatedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.RoutineComplex", new[] { "CreatedBy_Id" });
            DropColumn("dbo.RoutineComplex", "CreatedBy_Id");
        }
    }
}

namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParentWorkouts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoutineComplex", "ParentId", c => c.Int());
            CreateIndex("dbo.RoutineComplex", "ParentId");
            AddForeignKey("dbo.RoutineComplex", "ParentId", "dbo.RoutineComplex", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoutineComplex", "ParentId", "dbo.RoutineComplex");
            DropIndex("dbo.RoutineComplex", new[] { "ParentId" });
            DropColumn("dbo.RoutineComplex", "ParentId");
        }
    }
}

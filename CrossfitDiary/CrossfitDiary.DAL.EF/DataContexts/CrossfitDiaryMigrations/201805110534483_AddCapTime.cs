namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCapTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoutineComplex", "TimeCap", c => c.Time(precision: 7));
            AddColumn("dbo.CrossfitterWorkout", "RepsToFinishOnCapTime", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CrossfitterWorkout", "RepsToFinishOnCapTime");
            DropColumn("dbo.RoutineComplex", "TimeCap");
        }
    }
}

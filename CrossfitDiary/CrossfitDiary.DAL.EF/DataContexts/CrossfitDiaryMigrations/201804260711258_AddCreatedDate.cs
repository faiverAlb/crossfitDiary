namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoutineComplex", "CreatedUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.CrossfitterWorkout", "CreatedUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.RoutineSimple", "CreatedUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.Exercise", "CreatedUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.ExerciseMeasure", "CreatedUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.ExerciseMeasureType", "CreatedUtc", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExerciseMeasureType", "CreatedUtc");
            DropColumn("dbo.ExerciseMeasure", "CreatedUtc");
            DropColumn("dbo.Exercise", "CreatedUtc");
            DropColumn("dbo.RoutineSimple", "CreatedUtc");
            DropColumn("dbo.CrossfitterWorkout", "CreatedUtc");
            DropColumn("dbo.RoutineComplex", "CreatedUtc");
        }
    }
}

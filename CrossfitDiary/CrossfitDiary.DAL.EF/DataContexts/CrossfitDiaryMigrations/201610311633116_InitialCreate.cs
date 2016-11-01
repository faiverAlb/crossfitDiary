namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoutineComplex",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoundCount = c.Int(),
                        TimeToWork = c.Time(precision: 7),
                        RestBetweenExercises = c.Time(precision: 7),
                        RestBetweenRounds = c.Time(precision: 7),
                        Title = c.String(nullable: false),
                        ComplexType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoutineSimple",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExerciseId = c.Int(nullable: false),
                        RoutineComplexId = c.Int(nullable: false),
                        TimeToWork = c.Time(precision: 7),
                        Count = c.Int(),
                        Distance = c.Int(),
                        Weight = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercise", t => t.ExerciseId, cascadeDelete: true)
                .ForeignKey("dbo.RoutineComplex", t => t.RoutineComplexId, cascadeDelete: true)
                .Index(t => t.ExerciseId)
                .Index(t => t.RoutineComplexId);
            
            CreateTable(
                "dbo.Exercise",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExerciseMeasure",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExerciseMeasureTypeId = c.Int(nullable: false),
                        ExerciseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercise", t => t.ExerciseId, cascadeDelete: true)
                .ForeignKey("dbo.ExerciseMeasureType", t => t.ExerciseMeasureTypeId, cascadeDelete: true)
                .Index(t => t.ExerciseMeasureTypeId)
                .Index(t => t.ExerciseId);
            
            CreateTable(
                "dbo.ExerciseMeasureType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeasureType = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Crossfitter",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CrossfitterWorkout",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoutineComplexId = c.Int(nullable: false),
                        CrossfitterId = c.Int(nullable: false),
                        RoundsFinished = c.Int(),
                        TimePassed = c.Time(precision: 7),
                        Distance = c.Int(),
                        Date = c.DateTime(nullable: false),
                        Points = c.Int(),
                        WasFinished = c.Boolean(nullable: false),
                        IsRx = c.Boolean(nullable: false),
                        IsModified = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Crossfitter", t => t.CrossfitterId, cascadeDelete: true)
                .ForeignKey("dbo.RoutineComplex", t => t.RoutineComplexId, cascadeDelete: true)
                .Index(t => t.RoutineComplexId)
                .Index(t => t.CrossfitterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CrossfitterWorkout", "RoutineComplexId", "dbo.RoutineComplex");
            DropForeignKey("dbo.CrossfitterWorkout", "CrossfitterId", "dbo.Crossfitter");
            DropForeignKey("dbo.RoutineSimple", "RoutineComplexId", "dbo.RoutineComplex");
            DropForeignKey("dbo.RoutineSimple", "ExerciseId", "dbo.Exercise");
            DropForeignKey("dbo.ExerciseMeasure", "ExerciseMeasureTypeId", "dbo.ExerciseMeasureType");
            DropForeignKey("dbo.ExerciseMeasure", "ExerciseId", "dbo.Exercise");
            DropIndex("dbo.CrossfitterWorkout", new[] { "CrossfitterId" });
            DropIndex("dbo.CrossfitterWorkout", new[] { "RoutineComplexId" });
            DropIndex("dbo.ExerciseMeasure", new[] { "ExerciseId" });
            DropIndex("dbo.ExerciseMeasure", new[] { "ExerciseMeasureTypeId" });
            DropIndex("dbo.RoutineSimple", new[] { "RoutineComplexId" });
            DropIndex("dbo.RoutineSimple", new[] { "ExerciseId" });
            DropTable("dbo.CrossfitterWorkout");
            DropTable("dbo.Crossfitter");
            DropTable("dbo.ExerciseMeasureType");
            DropTable("dbo.ExerciseMeasure");
            DropTable("dbo.Exercise");
            DropTable("dbo.RoutineSimple");
            DropTable("dbo.RoutineComplex");
        }
    }
}

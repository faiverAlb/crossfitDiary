namespace CrossfitDiary.DAL.EF.DataContexts.IdentityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropPrimaryKey("dbo.AspNetUserClaims");
            CreateTable(
                "dbo.RoutineComplex",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoundCount = c.Int(),
                        TimeToWork = c.Time(precision: 7),
                        RestBetweenExercises = c.Time(precision: 7),
                        RestBetweenRounds = c.Time(precision: 7),
                        Title = c.String(),
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
                        Title = c.String(),
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
                        Description = c.String(),
                        ShortMeasureDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUserRoles", "IdentityUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUserClaims", "IdentityUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUserLogins", "IdentityUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.AspNetUserClaims", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.AspNetUserClaims", "UserId");
            CreateIndex("dbo.AspNetUserRoles", "IdentityUser_Id");
            CreateIndex("dbo.AspNetUserClaims", "IdentityUser_Id");
            CreateIndex("dbo.AspNetUserLogins", "IdentityUser_Id");
            CreateIndex("dbo.CrossfitterWorkout", "RoutineComplexId");
            AddForeignKey("dbo.CrossfitterWorkout", "RoutineComplexId", "dbo.RoutineComplex", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CrossfitterWorkout", "RoutineComplexId", "dbo.RoutineComplex");
            DropForeignKey("dbo.RoutineSimple", "RoutineComplexId", "dbo.RoutineComplex");
            DropForeignKey("dbo.RoutineSimple", "ExerciseId", "dbo.Exercise");
            DropForeignKey("dbo.ExerciseMeasure", "ExerciseMeasureTypeId", "dbo.ExerciseMeasureType");
            DropForeignKey("dbo.ExerciseMeasure", "ExerciseId", "dbo.Exercise");
            DropIndex("dbo.ExerciseMeasure", new[] { "ExerciseId" });
            DropIndex("dbo.ExerciseMeasure", new[] { "ExerciseMeasureTypeId" });
            DropIndex("dbo.RoutineSimple", new[] { "RoutineComplexId" });
            DropIndex("dbo.RoutineSimple", new[] { "ExerciseId" });
            DropIndex("dbo.CrossfitterWorkout", new[] { "RoutineComplexId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityUser_Id" });
            DropPrimaryKey("dbo.AspNetUserClaims");
            AlterColumn("dbo.AspNetUserClaims", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.AspNetUserLogins", "IdentityUser_Id");
            DropColumn("dbo.AspNetUserClaims", "IdentityUser_Id");
            DropColumn("dbo.AspNetUserRoles", "IdentityUser_Id");
            DropTable("dbo.ExerciseMeasureType");
            DropTable("dbo.ExerciseMeasure");
            DropTable("dbo.Exercise");
            DropTable("dbo.RoutineSimple");
            DropTable("dbo.RoutineComplex");
            AddPrimaryKey("dbo.AspNetUserClaims", "Id");
            CreateIndex("dbo.AspNetUserLogins", "UserId");
            CreateIndex("dbo.AspNetUserClaims", "UserId");
            CreateIndex("dbo.AspNetUserRoles", "UserId");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}

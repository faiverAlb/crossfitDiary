namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                        ShortMeasureDescription = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                        Discriminator = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.CrossfitterWorkout",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoutineComplexId = c.Int(nullable: false),
                        RoundsFinished = c.Int(),
                        PartialRepsFinished = c.Int(),
                        TimePassed = c.Time(precision: 7),
                        Distance = c.Int(),
                        Date = c.DateTime(nullable: false),
                        Points = c.Int(),
                        WasFinished = c.Boolean(nullable: false),
                        IsRx = c.Boolean(nullable: false),
                        IsModified = c.Boolean(nullable: false),
                        Crossfitter_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoutineComplex", t => t.RoutineComplexId, cascadeDelete: true)
                .ForeignKey("dbo.Crossfitter", t => t.Crossfitter_Id)
                .Index(t => t.RoutineComplexId)
                .Index(t => t.Crossfitter_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.IdentityUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Crossfitter",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Crossfitter", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "IdentityRole_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.CrossfitterWorkout", "Crossfitter_Id", "dbo.Crossfitter");
            DropForeignKey("dbo.CrossfitterWorkout", "RoutineComplexId", "dbo.RoutineComplex");
            DropForeignKey("dbo.RoutineSimple", "RoutineComplexId", "dbo.RoutineComplex");
            DropForeignKey("dbo.RoutineSimple", "ExerciseId", "dbo.Exercise");
            DropForeignKey("dbo.ExerciseMeasure", "ExerciseMeasureTypeId", "dbo.ExerciseMeasureType");
            DropForeignKey("dbo.ExerciseMeasure", "ExerciseId", "dbo.Exercise");
            DropIndex("dbo.Crossfitter", new[] { "Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.CrossfitterWorkout", new[] { "Crossfitter_Id" });
            DropIndex("dbo.CrossfitterWorkout", new[] { "RoutineComplexId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.ExerciseMeasure", new[] { "ExerciseId" });
            DropIndex("dbo.ExerciseMeasure", new[] { "ExerciseMeasureTypeId" });
            DropIndex("dbo.RoutineSimple", new[] { "RoutineComplexId" });
            DropIndex("dbo.RoutineSimple", new[] { "ExerciseId" });
            DropTable("dbo.Crossfitter");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.CrossfitterWorkout");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ExerciseMeasureType");
            DropTable("dbo.ExerciseMeasure");
            DropTable("dbo.Exercise");
            DropTable("dbo.RoutineSimple");
            DropTable("dbo.RoutineComplex");
        }
    }
}

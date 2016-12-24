namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExerciseAbbreviation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercise", "Abbreviation", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercise", "Abbreviation");
        }
    }
}

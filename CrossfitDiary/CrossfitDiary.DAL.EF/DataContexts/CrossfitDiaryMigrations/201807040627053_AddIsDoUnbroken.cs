namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDoUnbroken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoutineSimple", "IsDoUnbroken", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoutineSimple", "IsDoUnbroken");
        }
    }
}

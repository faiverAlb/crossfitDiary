namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_IsAlternative_Property_Exercise : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoutineSimple", "IsAlternative", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoutineSimple", "IsAlternative");
        }
    }
}

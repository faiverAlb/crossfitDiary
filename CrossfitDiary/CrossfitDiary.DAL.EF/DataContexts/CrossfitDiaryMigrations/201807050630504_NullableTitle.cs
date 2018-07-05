namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableTitle : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RoutineComplex", "Title", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RoutineComplex", "Title", c => c.String(nullable: false));
        }
    }
}

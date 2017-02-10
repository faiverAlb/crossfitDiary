namespace CrossfitDiary.DAL.EF.DataContexts.CrossfitDiaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addcalories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoutineSimple", "Calories", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoutineSimple", "Calories");
        }
    }
}

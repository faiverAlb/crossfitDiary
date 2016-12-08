namespace CrossfitDiary.DAL.EF.DataContexts.IdentityMigrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<CrossfitDiary.DAL.EF.DataContexts.IdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContexts\IdentityMigrations";
        }

        protected override void Seed(CrossfitDiary.DAL.EF.DataContexts.IdentityDbContext context)
        {
        }
    }
}

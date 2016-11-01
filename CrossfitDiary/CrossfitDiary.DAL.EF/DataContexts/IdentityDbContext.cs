using Microsoft.AspNet.Identity.EntityFramework;

namespace CrossfitDiary.DAL.EF.DataContexts
{

    public class IdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityDbContext() : base("CrossfitDiaryEntities", throwIfV1Schema: false)
        {
//            Database.SetInitializer(new IdentityDropCreateInitializer());
        }

        public static IdentityDbContext Create()
        {
            return new IdentityDbContext();
        }
    }
}
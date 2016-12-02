using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CrossfitDiary.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CrossfitDiary.DAL.EF.DataContexts
{

    public class IdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityDbContext() : base("CrossfitDiaryEntities", throwIfV1Schema: false)
        {
            //            Database.SetInitializer(new IdentityDropCreateInitializer());
//            Configuration.ProxyCreationEnabled = false;
        }

//        public static IdentityDbContext Create()
//        {
//            return new IdentityDbContext();
//        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
//            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
//            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId }).ToTable("AspNetUserLogins");
//            modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id).ToTable("AspNetRoles");
//            modelBuilder.Entity<IdentityUserRole>().HasKey(ur => new { ur.UserId, ur.RoleId }).ToTable("AspNetUserRoles");
//            modelBuilder.Entity<IdentityUserClaim>().HasKey(claim => claim.UserId).ToTable("AspNetUserClaims");
//            modelBuilder.Entity<IdentityUser>().HasKey(user => user.Id).ToTable("AspNetUsers");
        }
    }
}
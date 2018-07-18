using CrossfitDiaryCore.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrossfitDiaryCore.DAL.EF
{
    public class WorkouterContext: IdentityDbContext
    {
        
    }


    public class IdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityDbContext()
        {

        }
        public IdentityDbContext(DbContextOptions options) : base(options)
        {

        }
    }

}
using System.Linq;
using System.Threading.Tasks;
using CrossfitDiaryCore.DAL.EF;
using CrossfitDiaryCore.Model;

namespace CrossfitDiaryCore.BL.Services
{
    public class ManagerUsersService
    {
        private readonly WorkouterContext _workouterContext;
        public ManagerUsersService(WorkouterContext workouterContext)
        {
            _workouterContext = workouterContext;
        }

        public async Task SetShowOnlyUserWods(string userId, bool showOnlyUserWods)
        {
            ApplicationUser user =  _workouterContext.Users.Single(x => x.Id == userId);
            user.ShowOnlyUserWods = showOnlyUserWods;
            _workouterContext.Users.Update(user);
            await _workouterContext.SaveChangesAsync();
        }
    }
}
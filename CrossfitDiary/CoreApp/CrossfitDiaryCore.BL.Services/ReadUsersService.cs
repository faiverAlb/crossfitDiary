using System.Linq;
using CrossfitDiaryCore.DAL.EF;

namespace CrossfitDiaryCore.BL.Services
{
    /// <summary>
    ///     Read-only service to handle user-related queries
    /// </summary>
    public class ReadUsersService
    {
        private readonly WorkouterContext _workouterContext;

        public ReadUsersService(WorkouterContext workouterContext)
        {
            _workouterContext = workouterContext;
        }

        public bool CanUserPlanWorkouts(string userId)
        {
            return _workouterContext.Users.Single(x => x.Id == userId).CanPlanWorkouts;
        }

        public bool GetShowOnlyUserWods(string userId)
        {
            return _workouterContext.Users.Single(x => x.Id == userId).ShowOnlyUserWods;
        }
    }
}
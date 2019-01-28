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
            return true;
        }
    }
}
using CrossfitDiary.DAL.EF.Infrastructure;
using CrossfitDiary.Model;

namespace CrossfitDiary.DAL.EF.Repositories
{
    public interface ICrossfitterWorkoutRepository : IRepository<CrossfitterWorkout>
    {
        
    }

    public class CrossfitterWorkoutRepository : RepositoryBase<CrossfitterWorkout>, ICrossfitterWorkoutRepository
    {
        public CrossfitterWorkoutRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
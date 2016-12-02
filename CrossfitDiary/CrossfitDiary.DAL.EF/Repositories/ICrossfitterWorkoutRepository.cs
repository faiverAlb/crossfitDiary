using CrossfitDiary.DAL.EF.DataContexts;
using CrossfitDiary.DAL.EF.Infrastructure;
using CrossfitDiary.Model;

namespace CrossfitDiary.DAL.EF.Repositories
{
    public interface ICrossfitterWorkoutRepository : IRepository<CrossfitterWorkout>
    {
        
    }

    public class CrossfitterWorkoutRepository : RepositoryBase<CrossfitterWorkout>, ICrossfitterWorkoutRepository
    {
        public CrossfitterWorkoutRepository(IDbFactory dbFactory, CrossfitDiaryDbContext dbContext) : base(dbFactory)
        {
        }

        public override void AddOrUpdate(CrossfitterWorkout entity)
        {
            entity.Crossfitter = DbContext.Users.Find(entity.Crossfitter.Id);
            base.AddOrUpdate(entity);
        }
    }
}
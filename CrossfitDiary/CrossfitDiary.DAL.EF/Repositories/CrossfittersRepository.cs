using CrossfitDiary.DAL.EF.Infrastructure;
using CrossfitDiary.Model;

namespace CrossfitDiary.DAL.EF.Repositories
{
    public interface ICrossfittersRepository : IRepository<Crossfitter>
    {
    }

    public class CrossfittersRepository : RepositoryBase<Crossfitter>, ICrossfittersRepository
    {
        public CrossfittersRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
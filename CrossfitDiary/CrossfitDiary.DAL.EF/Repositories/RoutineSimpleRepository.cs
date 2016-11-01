using CrossfitDiary.DAL.EF.Infrastructure;
using CrossfitDiary.Model;

namespace CrossfitDiary.DAL.EF.Repositories
{
    public interface IRoutineSimpleRepository : IRepository<RoutineSimple>
    {
        
    }

    public class RoutineSimpleRepository : RepositoryBase<RoutineSimple>, IRoutineSimpleRepository
    {
        public RoutineSimpleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
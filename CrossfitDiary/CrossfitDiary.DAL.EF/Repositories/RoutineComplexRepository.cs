using CrossfitDiary.DAL.EF.Infrastructure;
using CrossfitDiary.Model;

namespace CrossfitDiary.DAL.EF.Repositories
{
    public interface IRoutineComplexRepository : IRepository<RoutineComplex> { }

    public class RoutineComplexRepository : RepositoryBase<RoutineComplex>, IRoutineComplexRepository
    {
        public RoutineComplexRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
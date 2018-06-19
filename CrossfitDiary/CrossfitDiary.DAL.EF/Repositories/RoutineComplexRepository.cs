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

        public override void Add(RoutineComplex entity)
        {
            entity.CreatedBy = DbContext.Users.Find(entity.CreatedBy.Id);
            int index = 0;
            foreach (RoutineComplex routineComplex in entity.Children)
            {
                routineComplex.CreatedBy = DbContext.Users.Find(entity.CreatedBy.Id);
                routineComplex.Position = index++;
            }

            base.Add(entity);
        }

    }
}
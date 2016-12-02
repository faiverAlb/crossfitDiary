using CrossfitDiary.DAL.EF.Infrastructure;
using CrossfitDiary.Model;

namespace CrossfitDiary.DAL.EF.Repositories
{
    public interface IExerciseRepository : IRepository<Exercise>
    {
    }

    public class ExerciseRepository: RepositoryBase<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
    


}
using CrossfitDiary.DAL.EF.DataContexts;

namespace CrossfitDiary.DAL.EF.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private CrossfitDiaryDbContext _dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public CrossfitDiaryDbContext DbContext
        {
            get { return _dbContext ?? (_dbContext = _dbFactory.Init()); }
        }
        public void Commit()
        {
            DbContext.Commit();
        }
    }
}
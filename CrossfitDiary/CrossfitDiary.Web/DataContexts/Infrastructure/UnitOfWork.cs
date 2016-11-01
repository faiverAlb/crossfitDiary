namespace CrossfitDiary.DAL.EF.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private CrossfitDiaryEntities _dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public CrossfitDiaryEntities DbContext
        {
            get { return _dbContext ?? (_dbContext = _dbFactory.Init()); }
        }
        public void Commit()
        {
            DbContext.Commit();
        }
    }
}
using CrossfitDiary.DAL.EF.DataContexts;

namespace CrossfitDiary.DAL.EF.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private CrossfitDiaryDbContext _dbContext;

        public CrossfitDiaryDbContext Init()
        {
            return _dbContext ?? (_dbContext = new CrossfitDiaryDbContext());
        }

        protected override void DisposeCore()
        {
            _dbContext?.Dispose();
        }
    }
}
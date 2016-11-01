using CrossfitDiary.DAL.EF;
using CrossfitDiary.DAL.EF.Infrastructure;

namespace CrossfitDiary.Web.DataContexts.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private CrossfitDiaryEntities _dbContext;

        public CrossfitDiaryEntities Init()
        {
            return _dbContext ?? (_dbContext = new CrossfitDiaryEntities());
        }

        protected override void DisposeCore()
        {
            _dbContext?.Dispose();
        }
    }
}
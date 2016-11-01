namespace CrossfitDiary.DAL.EF.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
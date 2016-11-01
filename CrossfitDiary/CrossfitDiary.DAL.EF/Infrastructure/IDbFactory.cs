using System;
using CrossfitDiary.DAL.EF.DataContexts;

namespace CrossfitDiary.DAL.EF.Infrastructure
{
    public interface IDbFactory: IDisposable
    {
        CrossfitDiaryDbContext Init();
    }
}
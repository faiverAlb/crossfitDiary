using System;
using System.Data.Entity;

namespace CrossfitDiary.DAL.EF.Infrastructure
{
    public interface IDbFactory: IDisposable
    {
        DbContext Init();
    }
}
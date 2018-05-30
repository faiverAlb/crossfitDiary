using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CrossfitDiary.DAL.EF.Infrastructure
{
    public interface IRepository<T> where T: class
    {
        void Add(T entity);
        void AddOrUpdate(T entity);

        void Update(T entity);

        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);

        T GetById(int id);

        T FirstOrDefault(Expression<Func<T, bool>> where);

        T Single(Expression<Func<T, bool>> where);
        T SingleOrDefault(Expression<Func<T, bool>> where);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetMany(Expression<Func<T,bool>> where);
    }

}
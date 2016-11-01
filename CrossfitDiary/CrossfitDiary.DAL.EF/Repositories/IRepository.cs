using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CrossfitDiary.DAL.EF.Infrastructure
{
    public interface IRepository<T> where T: class
    {
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);

        T GetById(int id);

        T Get(Expression<Func<T, bool>> where);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetMany(Expression<Func<T,bool>> where);
    }

    public interface IRepositoryAsync<T> where T: class
    {
        Task<TResult> GetAsync<TResult>(Func<IQueryable<T>, TResult> queary, CancellationToken cancellationToken);
    }
}
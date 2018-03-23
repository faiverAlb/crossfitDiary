using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using CrossfitDiary.DAL.EF.DataContexts;

namespace CrossfitDiary.DAL.EF.Infrastructure
{
    public abstract class RepositoryBase<T> where T: class 
    {
        private CrossfitDiaryDbContext _dataContext;
        private readonly DbSet<T> _dbSet;
        protected IDbFactory DbFactory { get; private set; }

        protected CrossfitDiaryDbContext DbContext
        {
            get { return _dataContext ?? (_dataContext = DbFactory.Init()); }
        }

        public RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dbSet = DbContext.Set<T>();
        }

        public virtual void AddOrUpdate(T entity)
        {
            _dbSet.AddOrUpdate(entity);
        }
        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbSet.Where(where).AsEnumerable();
            foreach (T obj in objects)
            {
                _dbSet.Remove(obj);
            }
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).ToList();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> where)
        {
            return _dbSet.FirstOrDefault(where);
        }

    }
}
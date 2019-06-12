using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Sam.ToolStock.DataProvider.Contexts;
using Sam.ToolStock.DataProvider.Interfaces;

namespace Sam.ToolStock.DataProvider.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity>, IDisposable 
        where TEntity : class
    {
        private readonly ToolContext _toolContext;
        private bool _disposed;

        public GenericRepository(ToolContext toolContext)
        {
            _toolContext = toolContext;
        }

        public virtual void Create(TEntity item)
        {
            _toolContext.Set<TEntity>().Add(item);
        }

        public virtual TEntity GetById(string id)
        {
            return _toolContext.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _toolContext.Set<TEntity>().ToList();
        }

        public virtual IEnumerable<TEntity> GetWhere(Func<TEntity, bool> predicate)
        {
            return _toolContext.Set<TEntity>().Where(predicate).ToList();
        }

        public virtual IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public virtual IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.AsEnumerable().Where(predicate).ToList();
        }

        public virtual void Delete(TEntity item)
        {
            _toolContext.Set<TEntity>().Remove(item);
        }

        public virtual void Update(TEntity item)
        {
            _toolContext.Entry(item).State = EntityState.Modified;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _toolContext.Dispose();
            }

            _disposed = true;
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _toolContext.Set<TEntity>();
            return includeProperties.Aggregate(query,
                (current, includeProperty) => current.Include(includeProperty));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sam.ToolStock.DataProvider.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Create(TEntity item);

        TEntity GetById(string id);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetWhere(Func<TEntity, bool> predicate);

        IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);

        IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties);

        void Delete(TEntity item);

        void Update(TEntity item);
    }
}

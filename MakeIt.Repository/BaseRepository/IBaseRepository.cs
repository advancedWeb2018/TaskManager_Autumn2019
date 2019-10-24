using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MakeIt.Repository.BaseRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        ICollection<TType> Get<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select) where TType : class;
        ICollection<TType> Get<TType>(Expression<Func<TEntity, TType>> select) where TType : class;
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);

        void Delete(int id);
        void Delete(TEntity entity);
    }
}

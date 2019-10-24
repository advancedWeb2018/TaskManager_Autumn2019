using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MakeIt.Repository.BaseRepository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;

        // ctor
        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        #region Retrieve Methods
        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public ICollection<TType> Get<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select) where TType : class
        {
            return _context.Set<TEntity>().Where(where).Select(select).ToList();
        }

        public ICollection<TType> Get<TType>(Expression<Func<TEntity, TType>> select) where TType : class
        {
            return _context.Set<TEntity>().Select(select).ToList();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().SingleOrDefault(predicate);
        }
        #endregion

        #region Update Methods
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }
        #endregion

        #region Create Methods
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }
        #endregion

        #region Delete Methods
        public void Delete(int id)
        {
            TEntity ent = _context.Set<TEntity>().Find(id);
            _context.Set<TEntity>().Remove(ent);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        #endregion
    }
}

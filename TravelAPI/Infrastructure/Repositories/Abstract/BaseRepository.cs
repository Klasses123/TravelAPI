using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TravelAPI.Database;
using TravelAPI.Infrastructure.Interfaces;

namespace TravelAPI.Infrastructure.Repositories.Abstract
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, IDataModel
    {
        private bool _disposed;
        private DbMainContext DbMainContext { get; }
        protected abstract DbSet<T> DbEntities { get; }

        protected BaseRepository(DbMainContext dbMainContext)
        {
            DbMainContext = dbMainContext;
        }

        public abstract T Create(T item);

        public abstract Task<T> CreateAsync(T item);

        public abstract void CreateRange(IEnumerable<T> items);

        public abstract Task CreateRangeAsync(IEnumerable<T> items);

        public abstract void Delete(Guid id);

        public abstract void Delete(T item);

        public abstract Task DeleteAsync(Guid id);

        public abstract Task DeleteAsync(T item);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DbMainContext.Dispose();
                }
            }
            _disposed = true;
        }

        public abstract ICollection<T> GetAll();

        public abstract T GetById(Guid id);

        public abstract Task<T> GetByIdAsync(Guid id);

        public IQueryable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return Include(includeProperties);
        }

        public IQueryable<T> GetWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate);
        }

        protected virtual IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbEntities.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public virtual void Save()
        {
            DbMainContext.SaveChanges();
        }

        public async virtual Task SaveAsync()
        {
            await DbMainContext.SaveChangesAsync();
        }

        public abstract T Update(T item);

        public abstract Task<T> UpdateAsync(T item);
    }
}

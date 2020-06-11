using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TravelAPI.Infrastructure.Interfaces
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {
        ICollection<T> GetAll();
        T GetById(Guid id);
        Task<T> GetByIdAsync(Guid id);
        T Create(T item);
        Task<T> CreateAsync(T item);
        void CreateRange(IEnumerable<T> items);
        Task CreateRangeAsync(IEnumerable<T> items);
        T Update(T item);
        Task<T> UpdateAsync(T item);
        void Delete(Guid id);
        void Delete(T item);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(T item);
        void Save();
        Task SaveAsync();
        IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
    }
}

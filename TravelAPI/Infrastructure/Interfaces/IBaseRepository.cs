using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TravelAPI.Infrastructure.Interfaces
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        T GetFirstWhere(Expression<Func<T, bool>> predicate);
        Task<T> GetFirstWhereAsync(Expression<Func<T, bool>> predicate);
        T GetById(Guid id);
        T GetById(string id);
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByIdAsync(string id);
        T Create(T item);
        Task<T> CreateAsync(T item);
        void CreateRange(IEnumerable<T> items);
        Task CreateRangeAsync(IEnumerable<T> items);
        T Update(T item);
        Task<T> UpdateAsync(T item);
        bool Delete(Guid id);
        void Delete(T item);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(T item);
        void DeleteRange(IEnumerable<T> range);
        Task DeleteRangeAsync(IEnumerable<T> range);
        void Save();
        Task SaveAsync();
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
    }
}

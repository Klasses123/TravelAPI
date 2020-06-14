using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TravelAPI.Common.Exceptions.ClientExceptions;
using TravelAPI.Database;
using TravelAPI.Infrastructure.Interfaces;
using TravelAPI.Infrastructure.Repositories.Abstract;

namespace TravelAPI.Infrastructure.Repositories
{
    public class GenericRepository<TEntity>: BaseRepository<TEntity> where TEntity : class, IDataModel
    {
        protected override DbSet<TEntity> DbEntities { get; }
        protected DbMainContext Context { get; }

        public GenericRepository(DbMainContext context) : base(context)
        {
            DbEntities = context.Set<TEntity>();
            Context = context;
        }

        public override IQueryable<TEntity> GetAll()
        {
            return DbEntities.AsNoTracking();
        }

        public override IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return DbEntities.Where(predicate).AsNoTracking();
        }

        public async override Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Run(() => GetAll(predicate));
        }

        public override TEntity GetById(Guid id)
        {
            return DbEntities.Find(id);
        }

        public override TEntity GetById(string id)
        {
            return DbEntities.Find(id);
        }

        public override async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await DbEntities.FindAsync(id);
        }

        public override async Task<TEntity> GetByIdAsync(string id)
        {
            return await DbEntities.FindAsync(id);
        }

        public override TEntity Create(TEntity item)
        {
            return DbEntities.Add(item).Entity;
        }
        public override void CreateRange(IEnumerable<TEntity> items)
        {
            DbEntities.AddRange(items);
        }

        public override async Task<TEntity> CreateAsync(TEntity item)
        {
            return (await DbEntities.AddAsync(item)).Entity;
        }
        public override async Task CreateRangeAsync(IEnumerable<TEntity> items)
        {
            await DbEntities.AddRangeAsync(items);
        }

        public override TEntity Update(TEntity item)
        {
            var updatedItem = DbEntities.Update(item);
            Context.SaveChanges();
            return updatedItem.Entity;
        }

        public override bool Delete(Guid id)
        {
            var item = GetById(id);
            if (item == null)
                return false;
            Delete(item);
            return true;
        }

        public override void Delete(TEntity item)
        {
            DbEntities.Remove(item);
        }

        public async override Task DeleteAsync(Guid id)
        {
            await Task.Run(() => Delete(id));
        }

        public async override Task DeleteAsync(TEntity item)
        {
            await Task.Run(() => DeleteAsync(item));
        }

        public async override Task<TEntity> UpdateAsync(TEntity item)
        {
            return await Task.Run(() => Update(item));
        }
    }
}

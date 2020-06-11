using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public GenericRepository(DbMainContext context) : base(context)
        {
            DbEntities = context.Set<TEntity>();
        }

        public override IQueryable<TEntity> GetAll()
        {
            return DbEntities.AsNoTracking();
        }

        public override TEntity GetById(Guid id)
        {
            var result = DbEntities.Find(id);
            return result;
        }

        public override async Task<TEntity> GetByIdAsync(Guid id)
        {
            var result = await DbEntities.FindAsync(id);
            return result;
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
            return DbEntities.Update(item).Entity;
        }

        public override void Delete(Guid id)
        {
            var item = GetById(id);
            if (item == null)
                throw new NotFoundException("Не удалось удалить объект, т.к. он не был найден!");
            Delete(item);
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
            return await Task.Run(() => UpdateAsync(item));
        }
    }
}

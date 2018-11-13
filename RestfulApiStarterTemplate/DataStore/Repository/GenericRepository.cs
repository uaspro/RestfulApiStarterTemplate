using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestfulApiStarterTemplate.Models.Entities.Base;

namespace RestfulApiStarterTemplate.DataStore.Repository
{
    public class GenericRepository : IRepository
    {
        protected readonly DbContext Context;

        public GenericRepository(DbContext context)
        {
            Context = context;
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : BaseEntity
        {
            var entities = Context.Set<TEntity>();
            return entities;
        }

        public IEnumerable<TEntity> Get<TEntity>() where TEntity : BaseEntity
        {
            var entities = Context.Set<TEntity>();
            return entities.AsEnumerable();
        }

        public async Task<TEntity> Get<TEntity>(params object[] id) where TEntity : BaseEntity
        {
            var entities = Context.Set<TEntity>();
            return await entities.FindAsync(id);
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entities = Context.Set<TEntity>();
            entities.Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entities = Context.Set<TEntity>();
            entities.Update(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entities = Context.Set<TEntity>();
            entities.Remove(entity);
        }

        public async Task<int> SaveChanges()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestfulApiStarterTemplate.Models.Entities.Base;

namespace RestfulApiStarterTemplate.DataStore.Repository
{
    public interface IRepository : IDisposable
    {
        IQueryable<TEntity> Query<TEntity>() where TEntity : BaseEntity;
        IEnumerable<TEntity> Get<TEntity>() where TEntity : BaseEntity;
        Task<TEntity> Get<TEntity>(params object[] id) where TEntity : BaseEntity;
        Task Insert<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task Update<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task Delete<TEntity>(TEntity entity) where TEntity : BaseEntity;
    }
}

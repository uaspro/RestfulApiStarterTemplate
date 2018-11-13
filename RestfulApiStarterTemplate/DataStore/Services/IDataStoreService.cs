using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestfulApiStarterTemplate.Models.Entities.Base;

namespace RestfulApiStarterTemplate.DataStore.Services
{
    public interface IDataStoreService<TEntity> : IDisposable where TEntity : BaseEntity
    {
        IQueryable<TEntity> Query();
        IEnumerable<TEntity> Get();
        Task<TEntity> Get(params object[] id);
        Task<bool> Insert(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Delete(TEntity entity);
    }
}

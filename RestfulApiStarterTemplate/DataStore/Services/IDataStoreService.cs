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
        bool Insert(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        Task<int> SaveChanges();
    }
}

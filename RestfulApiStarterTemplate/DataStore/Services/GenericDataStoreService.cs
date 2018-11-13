using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RestfulApiStarterTemplate.DataStore.Repository;
using RestfulApiStarterTemplate.Models.Entities.Base;

namespace RestfulApiStarterTemplate.DataStore.Services
{
    public class GenericDataStoreService<TEntity> : IDataStoreService<TEntity> where TEntity : BaseEntity
    {
        private const string ErrorMessageTemplate = "Error, during {0} operation.";

        protected readonly IRepository Repository;
        protected readonly ILogger<GenericDataStoreService<TEntity>> Logger;

        public GenericDataStoreService(IRepository repository,
                                       ILogger<GenericDataStoreService<TEntity>> logger)
        {
            Repository = repository;
            Logger = logger;
        }

        public IQueryable<TEntity> Query()
        {
            return Repository.Query<TEntity>();
        }

        public IEnumerable<TEntity> Get()
        {
            try
            {
                return Repository.Get<TEntity>();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format(ErrorMessageTemplate, nameof(Get)));
            }

            return null;
        }

        public async Task<TEntity> Get(params object[] id)
        {
            try
            {
                return await Repository.Get<TEntity>(id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format(ErrorMessageTemplate, nameof(Get)));
            }

            return await Task.FromResult<TEntity>(null);
        }

        public bool Insert(TEntity entity)
        {
            var result = true;
            try
            {
                Repository.Insert(entity);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format(ErrorMessageTemplate, nameof(Insert)));

                result = false;
            }

            return result;
        }

        public bool Update(TEntity entity)
        {
            var result = true;
            try
            {
                Repository.Update(entity);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format(ErrorMessageTemplate, nameof(Update)));

                result = false;
            }

            return result;
        }

        public bool Delete(TEntity entity)
        {
            var result = true;
            try
            {
                Repository.Delete(entity);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format(ErrorMessageTemplate, nameof(Delete)));

                result = false;
            }

            return result;
        }

        public async Task<int> SaveChanges()
        {
            try
            {
                return await Repository.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format(ErrorMessageTemplate, nameof(SaveChanges)));
            }

            return await Task.FromResult(0);
        }

        public void Dispose()
        {
            Repository.Dispose();
        }
    }
}

using RestfulApiStarterTemplate.DataStore.Context;

namespace RestfulApiStarterTemplate.DataStore.Repository
{
    public class DefaultRepository : GenericRepository
    {
        public DefaultRepository(DefaultDbContext context) : base(context)
        {
        }
    }
}

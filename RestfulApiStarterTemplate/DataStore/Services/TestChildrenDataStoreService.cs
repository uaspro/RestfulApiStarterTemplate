using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using RestfulApiStarterTemplate.DataStore.Repository;
using RestfulApiStarterTemplate.Models.Entities;

namespace RestfulApiStarterTemplate.DataStore.Services
{
    public class TestChildrenDataStoreService : GenericDataStoreService<TestChild>, ITestChildrenDataStoreService
    {
        public TestChildrenDataStoreService(IRepository repository, ILogger<GenericDataStoreService<TestChild>> logger)
            : base(repository, logger)
        {
        }

        public IList<TestChild> GetChildrenForTest(int testId)
        {
            return Query().Where(c => c.TestId == testId).ToList();
        }

        public TestChild GetChildForTest(int testId, int id)
        {
            return Query().FirstOrDefault(c => c.TestId == testId && c.Id == id);
        }

        public void AddChildForTest(int testId, TestChild testChild)
        {
            testChild.TestId = testId;
            Insert(testChild);
        }
    }
}

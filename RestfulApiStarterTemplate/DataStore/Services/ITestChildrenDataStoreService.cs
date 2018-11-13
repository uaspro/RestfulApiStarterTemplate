using System.Collections.Generic;
using RestfulApiStarterTemplate.Models.Entities;

namespace RestfulApiStarterTemplate.DataStore.Services
{
    public interface ITestChildrenDataStoreService : IDataStoreService<TestChild>
    {
        IList<TestChild> GetChildrenForTest(int testId);
        TestChild GetChildForTest(int testId, int id);
        void AddChildForTest(int testId, TestChild testChild);
    }
}

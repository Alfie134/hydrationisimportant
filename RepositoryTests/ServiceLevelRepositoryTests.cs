using Microsoft.Data.SqlClient;
using Models;
using Repositories;
using Repositories.Interfaces;

namespace RepositoryTests
{
    [TestClass]
    public class ServiceLevelRepositoryTests: RepositoryTestBase
    {
        private IServiceLevelRepository _serviceLevelRepository;

        [TestInitialize]
        public void InitializeTest()
        {
            _serviceLevelRepository = new ServiceLevelRepository();
        }


        [TestMethod]
        public void GetAll_ReturnsAllRows()
        {
            IEnumerable<ServiceLevel> serviceLevelsList;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                serviceLevelsList = _serviceLevelRepository.GetAll(connection);
            }

            Assert.IsTrue(serviceLevelsList.Count() != 0);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectInfo()
        {
            ServiceLevel tempServiceLevel;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                tempServiceLevel = _serviceLevelRepository.GetById(1, connection);
                Assert.IsNotNull(tempServiceLevel);

            }
            Assert.IsTrue(tempServiceLevel.Name == "One Hour");
            Assert.IsTrue(tempServiceLevel.Time.TotalHours == 1);
        }

    }
}

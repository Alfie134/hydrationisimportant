using Models;
using Repositories;
using Repositories.Interfaces;

namespace RepositoryTests
{
    [TestClass]
    public class ServiceLevelRepositoryTests
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
            IEnumerable<ServiceLevel> serviceLevelsList = _serviceLevelRepository.GetAll();
            Assert.IsTrue(serviceLevelsList.Count() != 0);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectInfo()
        {
            ServiceLevel tempServiceLevel = _serviceLevelRepository.GetById(1);
            Assert.IsNotNull(tempServiceLevel);
            Assert.IsTrue(tempServiceLevel.Name == "One Hour");
            Assert.IsTrue(tempServiceLevel.Timespan.TotalHours == 1);
        }

    }
}

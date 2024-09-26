using Models;
using Repositories;
using Repositories.Interfaces;

namespace RepositoryTests
{
    [TestClass]
    public class RegionRepositoryTests
    {
        public TestContext TestContext { get; set; }
        private IRegionRepository _regionRepository;

        [TestInitialize]
        public void InitializeTest()
        {
            _regionRepository = new RegionRepository((string)TestContext.Properties["testdatabaseURL"]);
        }


        [TestMethod]
        public void GetAll_ReturnsAllRows()
        {
            IEnumerable<Region> regionsList = _regionRepository.GetAll();
            Assert.IsNotNull(regionsList.Count() != 0);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectInfo()
        {
            Region tempRegion = _regionRepository.GetById(1081);
            Assert.IsNotNull(tempRegion);
            Assert.IsTrue(tempRegion.Name == "Region Nordjylland");
        }
    }
}

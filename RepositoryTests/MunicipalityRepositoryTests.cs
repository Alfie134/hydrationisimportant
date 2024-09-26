using Models;
using Repositories;
using Repositories.Interfaces;

namespace RepositoryTests
{
    [TestClass]
    public class MunicipalityRepositoryTests
    {
        public TestContext TestContext { get; set; }
        private IMunicipalityRepository _municipalityRepository;

        [TestInitialize]
        public void InitializeTest()
        {
            _municipalityRepository = new MunicipalityRepository((string)TestContext.Properties["testdatabaseURL"]);
        }

        [TestMethod]
        public void GetAll_ReturnAllRows()
        {
            IEnumerable<Municipality> municipalityList = _municipalityRepository.GetAll();
            Assert.IsTrue(municipalityList.Count() != 0);
        }

        [TestMethod]
        public void GetById_ReturnCorrectInfo()
        {
            Municipality tempMunicipality = _municipalityRepository.GetById(1);
            Assert.IsNotNull(tempMunicipality);
            Assert.IsTrue(tempMunicipality.Name == "Silkebprg");
        }
    }
}

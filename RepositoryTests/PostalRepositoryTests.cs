using Models;
using Repositories;
using Repositories.Interfaces;

namespace RepositoryTests
{
    [TestClass]
    public class PostalRepositoryTests
    {
        public TestContext TestContext { get; set; }
        private IPostalRepository _postalRepository;

        [TestInitialize]
        public void InitializeTest()
        {
            _postalRepository = new PostalRepository((string)TestContext.Properties["testdatabaseURL"]);
        }


        [TestMethod]
        public void GetAll_ReturnsAllRows()
        {
            IEnumerable<Postal> postalsList = _postalRepository.GetAll();
            Assert.IsTrue(postalsList.Count() != 0);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectInfo()
        {
            Postal tempPostal = _postalRepository.GetById(783);
            Assert.IsNotNull(tempPostal);
            Assert.IsTrue(tempPostal.CityName == "Facility");
        }
    }
} 

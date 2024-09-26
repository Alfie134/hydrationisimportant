using Models;
using Repositories;
using Repositories.Interfaces;

namespace RepositoryTests
{
    public class RouteRepositoryTest
    {
        public TestContext TestContext { get; set; }
        private IRouteRepository _routeRepository;

        [TestInitialize]
        public void InitializeTest()
        {
            _routeRepository = new RouteRepository((string)TestContext.Properties["testdatabaseURL"]);
        }

        [TestMethod]
        public void GetAll_ReturnsAllRows()
        {
            IEnumerable<Route> routesList = _routeRepository.GetAll();
            Assert.IsNotNull(routesList.Count() != 0);
        }

//Mulige fejl grundet manglende data i databasen
    //Henholdsvis på linje 23 & 25

        [TestMethod]
        public void GetById_ReturnsCorrectInformation()
        {
            Route tempRoute = _routeRepository.GetById(0);
            Assert.IsNotNull(tempRoute);
            Assert.IsTrue(tempRoute.RouteId == 0);
        }
    }
}

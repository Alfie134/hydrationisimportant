using Models;
using Repositories;
using Repositories.Interfaces;

namespace RepositoryTests
{
    [TestClass]
    public class VehicleRepositoryTests
    {
        public TestContext TestContext { get; set; }
        private IVehicleRepository _vehicleRepository;

        [TestInitialize]
        public void InitializeTest()
        {
            _vehicleRepository = new VehicleRepository((string)TestContext.Properties["testdatabaseURL"]);
        }


        [TestMethod]
        public void GetAll_ReturnsAllRows()
        {
            IEnumerable<Vehicle> vehiclesList = _vehicleRepository.GetAll();
            Assert.IsTrue(vehiclesList.Count() != 0);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectInfo()
        {
            Vehicle tempVehicle = _vehicleRepository.GetById(1);
            Assert.IsNotNull(tempVehicle);
            Assert.IsTrue(tempVehicle.OperatorId == 1);
            Assert.IsTrue(tempVehicle.Type == VehicleType.Ambulance);
        }
    }
}


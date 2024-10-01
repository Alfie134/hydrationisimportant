using Models;
using Repositories;
using Repositories.Interfaces;

namespace RepositoryTests
{
    [TestClass]
    public class VehicleRepositoryTests
    {
        private IVehicleRepository _vehicleRepository;

        [TestInitialize]
        public void InitializeTest()
        {
            _vehicleRepository = new VehicleRepository();
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


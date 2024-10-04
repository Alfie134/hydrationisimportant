using Microsoft.Data.SqlClient;
using Models;
using Repositories;
using Repositories.Interfaces;

namespace RepositoryTests
{
    [TestClass]
    public class VehicleRepositoryTests: RepositoryTestBase
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
            IEnumerable<Vehicle> vehiclesList;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                // Act
                vehiclesList = _vehicleRepository.GetAll(connection);

                // Assert
                Assert.IsTrue(vehiclesList.Count() != 0);
            }
        }

        [TestMethod]
        public void GetById_ReturnsCorrectInfo()
        {
            Vehicle tempVehicle;

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                // Act
                tempVehicle = _vehicleRepository.GetById(1, connection);

            }

            // Assert

            Assert.IsNotNull(tempVehicle);
            Assert.IsTrue(tempVehicle.OperatorId == 1);
        }
    }
}


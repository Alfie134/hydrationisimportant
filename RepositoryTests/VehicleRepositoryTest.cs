using Configuration;
using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryTests
{
    [TestClass]
    public class VehicleRepositoryTest: RepositoryTestBase
    {
        private string _connectionString;
        private IVehicleRepository _vehicleRepository;

        [TestInitialize]
        public void Setup()
        {
            _connectionString = ConnectionString;
            _vehicleRepository = new VehicleRepository();
        }


        [TestMethod]
        public void Test_GetAll_ReturnsListOfVehicles()
        {
            // Arrange
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Act
                var vehicles = _vehicleRepository.GetAll(connection);

                // Assert
                Assert.IsNotNull(vehicles, "The result should not be null.");
                Assert.IsTrue(vehicles.Count() > 0, "The result should contain at least one vehicle.");
            }
        }
        [TestMethod]
        public void Test_GetById_ReturnsRegionSyd()
        {
            // Arrange
            int expectedId = 1; // Id for AMB0001
            string expectedName = "AMB001";
            int expectedOperator = 1;
            int expectedRegion = 1084;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Act
                var vehicle = _vehicleRepository.GetById(expectedId, connection);

                // Assert
                Assert.IsNotNull(vehicle, "The vehicle should not be null.");
                Assert.AreEqual(expectedId, vehicle.Id, "The Id does not match.");
                Assert.AreEqual(expectedName, vehicle.VehicleNumber, "The vehicleNumber does not match.");
                Assert.AreEqual(expectedOperator, vehicle.OperatorId, "The Operator does not match.");
                Assert.AreEqual(expectedRegion, vehicle.RegionId, "The Region Does not match");
            }
        }


    }
}


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
    public class ServiceLevelRepositoryTest: RepositoryTestBase
    {
        private string _connectionString;
        private IServiceLevelRepository _serviceLevelRepository;

        [TestInitialize]
        public void Setup()
        {
            _connectionString = ConnectionString;
            _serviceLevelRepository = new ServiceLevelRepository();
        }

        [TestMethod]
        public void Test_GetAll_ReturnsListOfServiceLevels()
        {
            // Arrange
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Act
                var serviceLevels = _serviceLevelRepository.GetAll(connection);

                // Assert
                Assert.IsNotNull(serviceLevels, "The result should not be null.");
                Assert.IsTrue(serviceLevels.Count() > 0, "The result should contain at least one service level.");
            }
        }

        [TestMethod]
        public void Test_GetById_ReturnsExpectedServiceLevel()
        {
            // Arrange
            int expectedId = 1; // Specific Id for a service level in your database
            string expectedName = "SERVICELAG 1"; // Replace with the actual expected service level name
            TimeSpan expectedTimeSpan = TimeSpan.FromMinutes(60); // Replace with the actual expected TimeSpan (in minutes)

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Act
                var serviceLevel = _serviceLevelRepository.GetById(expectedId, connection);

                // Assert
                Assert.IsNotNull(serviceLevel, "The service level should not be null.");
                Assert.AreEqual(expectedId, serviceLevel.Id, "The Id does not match.");
                Assert.AreEqual(expectedName, serviceLevel.Name, "The Name does not match.");
                Assert.AreEqual(expectedTimeSpan, serviceLevel.Time, "The TimeSpan does not match.");
            }
        }
    }
}

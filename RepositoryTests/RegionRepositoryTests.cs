using Configuration;
using Microsoft.Data.SqlClient;
using Models;
using Repositories;
using Repositories.Interfaces;

namespace RepositoryTests
{
    [TestClass]
    public class RegionRepositoryTests: RepositoryTestBase
    {
        private string _connectionString;
        private IRegionRepository _regionRepository;

        [TestInitialize]
        public void Setup()
        {
            _connectionString = ConnectionString;
            _regionRepository = new RegionRepository(_connectionString);
        }

        [TestMethod]
        public void Test_GetAllRegions_ReturnsListOfRegions()
        {
            // Arrange
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Act
                var regions = _regionRepository.GetAll(connection);

                // Assert
                Assert.IsNotNull(regions, "The result should not be null.");
                Assert.IsTrue(regions.Count() > 0, "The result should contain at least one municipality.");
            }
        }

        [TestMethod]
        public void Test_GetById_ReturnsRegionSyd()
        {
            // Arrange
            int expectedId = 1083; // Id for Region Syddanmark
            string expectedName = "Region Syddanmark";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Act
                var region = _regionRepository.GetById(expectedId, connection);

                // Assert
                Assert.IsNotNull(region, "The region should not be null.");
                Assert.AreEqual(expectedId, region.RegionId, "The Id does not match.");
                Assert.AreEqual(expectedName, region.Name, "The Name does not match.");
            }
        }


    }
}


using Configuration;
using Microsoft.Data.SqlClient;
using Models;
using Repositories;
using Repositories.Interfaces;

namespace RepositoryTests
{
    [TestClass]
    public class MunicipalityRepositoryTests: RepositoryTestBase
    {
        private string _connectionString;
        private IMunicipalityRepository _municipalityRepository;

        [TestInitialize]
        public void Setup()
        {
            _connectionString = ConnectionString;
            _municipalityRepository = new MunicipalityRepository(_connectionString);
        }

        [TestMethod]
        public void Test_GetAllMunicipalities_ReturnsListOfMunicipalities()
        {
            // Arrange
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Act
                var municipalities = _municipalityRepository.GetAll(connection);

                // Assert
                Assert.IsNotNull(municipalities, "The result should not be null.");
                Assert.IsTrue(municipalities.Count() > 0, "The result should contain at least one municipality.");
            }
        }

        [TestMethod]
        public void Test_GetById_ReturnsKøbenhavn()
        {
            // Arrange
            int expectedId = 101; // Id for Københavns Kommune
            string expectedName = "Københavns Kommune";
            int expectedRegionId = 1084; // RegionId for Københavns Kommune

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Act
                var municipality = _municipalityRepository.GetById(expectedId, connection);

                // Assert
                Assert.IsNotNull(municipality, "The municipality should not be null.");
                Assert.AreEqual(expectedId, municipality.Id, "The Id does not match.");
                Assert.AreEqual(expectedName, municipality.Name, "The Name does not match.");
                Assert.AreEqual(expectedRegionId, municipality.RegionId, "The RegionId does not match.");
            }
        }

        [TestMethod]
        public void Test_GetById_ReturnsRanders()
        {
            // Arrange
            int expectedId = 730; // Id for Randers Kommune
            string expectedName = "Randers Kommune";
            int expectedRegionId = 1082; // RegionId for Randers kommune

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Act
                var municipality = _municipalityRepository.GetById(expectedId, connection);

                // Assert
                Assert.IsNotNull(municipality, "The municipality should not be null.");
                Assert.AreEqual(expectedId, municipality.Id, "The Id does not match.");
                Assert.AreEqual(expectedName, municipality.Name, "The Name does not match.");
                Assert.AreEqual(expectedRegionId, municipality.RegionId, "The RegionId does not match.");
            }
        }

    }
}

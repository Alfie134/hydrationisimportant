using Configuration;
using Microsoft.Data.SqlClient;
using Models;
using Repositories;
using Repositories.Interfaces;

namespace RepositoryTests
{

    [TestClass]
    public class PostalRepositoryTests: RepositoryTestBase
    {
        private string _connectionString;
        private IPostalRepository _postalRepository;

        [TestInitialize]
        public void Setup()
        {
            _connectionString = ConnectionString;
            _postalRepository = new PostalRepository(_connectionString);
        }

        [TestMethod]
        public void Test_GetAllPostals_ReturnsListOfPostals()
        {
            // Arrange
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Act
                var postals = _postalRepository.GetAll(connection);

                // Assert
                Assert.IsNotNull(postals, "The result should not be null.");
                Assert.IsTrue(postals.Count() > 0, "The result should contain at least one municipality.");
            }
        }

        [TestMethod]
        public void Test_GetById_ReturnsRandersSV()
        {
            // Arrange
            int expectedId = 8940; // Id for Randers Kommune
            string expectedName = "Randers SV";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Act
                var postal = _postalRepository.GetById(expectedId, connection);

                // Assert
                Assert.IsNotNull(postal, "The municipality should not be null.");
                Assert.AreEqual(expectedId, postal.PostalNumber, "The Id does not match.");
                Assert.AreEqual(expectedName, postal.CityName, "The Name does not match.");
            }
        }
    }

} 

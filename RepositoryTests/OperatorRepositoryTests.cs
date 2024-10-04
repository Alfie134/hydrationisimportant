using Microsoft.Data.SqlClient;
using Models;
using Repositories;
using Repositories.Interfaces;

namespace RepositoryTests
{
    [TestClass]
    public class OperatorRepositoryTests: RepositoryTestBase
    {
        private IOperatorRepository _operatorRepository;
        private string _connectionString;
        private IMissionRepository _missionRepository;


        [TestInitialize]
        public void InitializeTest() 
        {
            _connectionString = ConnectionString;
            _operatorRepository = new OperatorRepository();
        }

        [TestMethod]
        public void GetAll_ReturnAllRows()
        {
            IEnumerable<Operator> operatorsList;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Act
                operatorsList = _operatorRepository.GetAll(connection);

            }

            // Assert
            Assert.IsTrue(operatorsList.Count()!= 0);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectInfo()
        {
            Operator tempOperator;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Act
                tempOperator = _operatorRepository.GetById(1, connection);

            }

            Assert.IsNotNull(tempOperator);
            Assert.IsTrue(tempOperator.Name == "Bent");
        }
    }
}

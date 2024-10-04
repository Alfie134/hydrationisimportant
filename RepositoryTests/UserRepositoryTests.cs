using Microsoft.Data.SqlClient;
using Models;
using Repositories;
using Repositories.Interfaces;

namespace RepositoryTests
{
    [TestClass]
    public class UserRepositoryTests: RepositoryTestBase
    {
        private IUserRepository _userRepository;

        [TestInitialize]
        public void InitializeTest()
        {
            _userRepository = new UserRepository();
        }

        [TestMethod]
        public void GetAll_ReturnAllRows()
        {
            IEnumerable<User> usersList;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                usersList = _userRepository.GetAll(connection);
            }


            Assert.IsTrue(usersList.Count() != 0);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectInfo()
        {
            User tempUser;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                tempUser = _userRepository.GetById(1, connection);
            }


            Assert.IsNotNull(tempUser);
            Assert.IsNotNull(tempUser.PasswordHash);
            Assert.IsTrue(tempUser.UserName == "Bente");
        }

        [TestMethod]
        public void GetByUsername_ReturnsCorrectInfo()
        {
            User tempUser;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                tempUser = _userRepository.GetByUsername("Bente", connection);
            }

            Assert.IsNotNull(tempUser);
            Assert.IsNotNull(tempUser.PasswordHash);
            Assert.IsTrue(tempUser.Id == 1);
        }
    }
}

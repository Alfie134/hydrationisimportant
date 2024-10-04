using Configuration;
using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces;
using Repositories;
using System;
using System.Linq;

namespace RepositoryTests
{
    [TestClass]
    public class UserRepositoryTest : RepositoryTestBase
    {
        private string _connectionString;
        private IUserRepository _userRepository;

        [TestInitialize]
        public void Setup()
        {
            _connectionString = ConnectionString;
            _userRepository = new UserRepository();
        }

        [TestMethod]
        public void CreateUser()
        {
            // Arrange
            User user = new User("TestBruger", "123Test456", 1083,"0");
            int id;

            // Act
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        id = _userRepository.Add(user, connection, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            // Assert
            Assert.IsTrue(id > 0);
        }

        [TestMethod]
        public void Test_GetAll_ReturnsListOfUsers()
        {
            // Arrange
            User user = new User("TestBrugerList", "password789", 1083,"0");
            int userId;

            // Add a user to ensure there's at least one user in the database
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        userId = _userRepository.Add(user, connection, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            // Act
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var users = _userRepository.GetAll(connection);

                // Assert
                Assert.IsNotNull(users, "The result should not be null.");
                Assert.IsTrue(users.Count() > 0, "The result should contain at least one user.");
            }
        }

        [TestMethod]
        public void Test_GetById_ReturnsNewlyCreatedUser()
        {
            // Arrange
            User user = new User("TestBrugerById", "password123", 1083,"0");
            int userId;

            // First, we create a user to retrieve later
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        userId = _userRepository.Add(user, connection, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            // Act: Retrieve the user by its Id
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var retrievedUser = _userRepository.GetById(userId, connection);

                // Assert
                Assert.IsNotNull(retrievedUser, "The user should not be null.");
                Assert.AreEqual(user.UserName, retrievedUser.UserName, "The username does not match.");
                Assert.AreEqual(user.PasswordHash, retrievedUser.PasswordHash, "The password does not match.");
                Assert.AreEqual(user.RegionId, retrievedUser.RegionId, "The region does not match.");
            }
        }
        [TestMethod]
        public void Test_GetByUserName_ReturnsNewlyCreatedUser()
        {
            // Arrange
            User user = new User("TestBrugerById", "password123", 1083, "0");
            int userId;

            // First, we create a user to retrieve later
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        userId = _userRepository.Add(user, connection, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            // Act: Retrieve the user by its Id
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var retrievedUser = _userRepository.GetByUsername(user.UserName, connection);

                // Assert
                Assert.IsNotNull(retrievedUser, "The user should not be null.");
                Assert.AreEqual(user.UserName, retrievedUser.UserName, "The username does not match.");
                Assert.AreEqual(user.PasswordHash, retrievedUser.PasswordHash, "The password does not match.");
                Assert.AreEqual(user.RegionId, retrievedUser.RegionId, "The region does not match.");
            }
        }
        [TestMethod]
        public void Test_DeleteUser_RemovesUserFromDatabase()
        {
            // Arrange
            User user = new User("TestDeleteUser", "password123", 1083,"0");
            int userId;

            // Act: Create the user first
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        userId = _userRepository.Add(user, connection, transaction);

                        // Confirm user was added
                        User addedUser = _userRepository.GetById(userId, connection, transaction);
                        Assert.IsNotNull(addedUser);

                        // Delete the user
                        bool result = _userRepository.Delete(addedUser, connection, transaction);
                        transaction.Commit();

                        // Assert
                        Assert.IsTrue(result, "User should be deleted.");

                        // Confirm the user is no longer in the database
                        User deletedUser = _userRepository.GetById(userId, connection);
                        Assert.IsNull(deletedUser, "The user should no longer exist in the database.");
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        [TestMethod]
        public void Test_DeleteUserById_RemovesUserFromDatabase()
        {
            // Arrange
            User user = new User("TestDeleteUserById", "password456", 1083,"0");
            int userId;

            // Act: Create the user first
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        userId = _userRepository.Add(user, connection, transaction);

                        // Confirm user was added
                        User addedUser = _userRepository.GetById(userId, connection, transaction);
                        Assert.IsNotNull(addedUser);

                        // Delete the user via id
                        bool result = _userRepository.DeleteById(userId, connection, transaction);
                        transaction.Commit();

                        // Assert
                        Assert.IsTrue(result, "User should be deleted.");

                        // Confirm the user is no longer in the database
                        User deletedUser = _userRepository.GetById(userId, connection);
                        Assert.IsNull(deletedUser, "The user should no longer exist in the database.");
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}

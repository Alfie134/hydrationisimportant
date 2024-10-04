using System.ComponentModel.Design;
using Configuration;
using Repositories;
using Repositories.Interfaces;
using System.Security.Cryptography;
using Microsoft.Data.SqlClient;
using Models;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Services
{
    public class UserService
    {
        public string SecretPepper;
        private IUserRepository _userRepository;
        private readonly string _connectionString;
        private AppConfig _appConfig;

        public UserService()
        {
            _appConfig = new AppConfig();
            SecretPepper = new AppConfig().PasswordPepper;
            _connectionString = _appConfig.ConnectionString;
            _userRepository = new UserRepository();

        }

        public User UserLogin(string username, string password)
        {
            User user = GetUserByUsername(username);
            byte[] Salt = Convert.FromBase64String(user.PasswordSalt);
            string HashedPassword = HashPasswordWithSaltAndPepper(password, Salt );

            if (HashedPassword == user.PasswordHash)
            {
                return user;
            }
            return null;
        }

        public User GetUserByUsername(string username)
        {
            User user;
            using (var connection = new SqlConnection(_appConfig.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        user = _userRepository.GetByUsername(username, connection, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return user;
        }

        public ServiceResult<User> CreateUser(string username, string password,int region)
        {
            byte[] Salt = GenerateSalt();
            string SaltAsString = Convert.ToBase64String(Salt);
            string passwordHash = HashPasswordWithSaltAndPepper(password,Salt);

                
                using (var connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();
                        User user = new User(username, passwordHash, region, SaltAsString);
                        int id = _userRepository.Add(user, connection);
                        user = _userRepository.GetById(id, connection);
                        return ServiceResult<User>.Success(user);
                    }
                    catch (Exception e)
                    {

                        return ServiceResult<User>.Failure(e.Message, 0);
                    }
                }

        }

            public static byte[] GenerateSalt(int size = 16)
            {
                byte[] salt = new byte[size];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                return salt;
            }

            public string HashPasswordWithSaltAndPepper(string password, byte[] salt)
            {
                string passwordWithPepper = password + SecretPepper;
                using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(passwordWithPepper, salt, 10000))
                {
                    byte[] hash = rfc2898DeriveBytes.GetBytes(20);
                    return Convert.ToBase64String(hash);
                }
            }
        
    }
}

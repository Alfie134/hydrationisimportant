using System.ComponentModel.Design;
using Configuration;
using Repositories;
using Repositories.Interfaces;
using System.Security.Cryptography;
using Microsoft.Data.SqlClient;
using Models;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Services
{
    public class UserService
    {
        public string SecretPepper;
        private readonly IUserRepository _userRepository;
        private readonly string _connectionString;
        private readonly AppConfig _appConfig;

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
            string hashedPassword = HashPasswordWithSaltAndPepper(password, user.PasswordSalt );

            if (hashedPassword == user.PasswordHash)
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
            string salt = GenerateSalt();
            string passwordHash = HashPasswordWithSaltAndPepper(password,salt);

                
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        User? existingUser = _userRepository.GetByUsername(username, connection, transaction);
                        if (existingUser != null)
                        {
                            throw new Exception($"User with username: {username} already exists");
                        }
                        User user = new User(username, passwordHash, region, salt);
                        int id = _userRepository.Add(user, connection, transaction);
                        user = _userRepository.GetById(id, connection, transaction);
                        transaction.Commit();
                        return ServiceResult<User>.Success(user);
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        return ServiceResult<User>.Failure(e.Message, 0);
                    }

                }
            }
        }

            public static string GenerateSalt(int size = 16)
            {
                byte[] salt = new byte[size];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                return Convert.ToBase64String(salt);
            }


            public  string HashPasswordWithSaltAndPepper(string password, string salt)
            {
                string saltedAndPepperedPassword = password + salt + SecretPepper;

                
                byte[] passwordBytes = Encoding.UTF8.GetBytes(saltedAndPepperedPassword);

                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                    // Convert to a readable hexadecimal string
                    StringBuilder sb = new StringBuilder();
                    foreach (byte b in hashBytes)
                    {
                        sb.Append(b.ToString("x2"));
                    }
                    return sb.ToString();
                }
            }
    }
}

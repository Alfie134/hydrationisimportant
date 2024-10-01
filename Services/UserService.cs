using Configuration;
using Models;
using Repositories;
using Repositories.Interfaces;

namespace Services
{
    internal class UserService
    {

        private readonly IUserRepository _userRepository;

        public UserService()
        {
            var config = new AppConfig();
            _userRepository = new UserRepository(config.ConnectionString);
        }
    
        public User UserLogin(string username, string password)
        {
            User user = _userRepository.GetByUserName(username);
            if (user != null)
            {
                if (user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }
    }
}

using Configuration;
using Models;
using Repositories;
using Repositories.Interfaces;

namespace Services
{
    public class UserService
    {

        private readonly IUserRepository _userRepository;

        public UserService()
        {
            var config = new AppConfig();
            _userRepository = new UserRepository();
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

using Models;

namespace Repositories.Interfaces
{
    public interface IUserRepository:IReadRepository<User>
    {
        public User GetByUserName(string userName);
    }
}

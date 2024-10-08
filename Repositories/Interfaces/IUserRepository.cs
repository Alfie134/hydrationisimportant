using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces.Base;

namespace Repositories.Interfaces
{
    public interface IUserRepository : ICreateRepository<User>, IReadRepository<User>, IUpdateRepository<User>, IDeleteRepository<User>
    {
        public User GetByUsername(string username, SqlConnection connection, SqlTransaction? transaction = null);
    }
}

using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Repositories.Interfaces
{
    public interface IUserRepository : ICreateRepository<User>, IReadRepository<User>, IUpdateRepository<User>, IDeleteRepository<User>
    {
        public User GetByUsername(string username, SqlConnection connection, SqlTransaction? transaction = null);
    }
}

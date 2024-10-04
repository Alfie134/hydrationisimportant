using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Repositories.Interfaces.Base
{
    public interface IReadRepository<out T>
    {
        IEnumerable<T> GetAll(SqlConnection connection, SqlTransaction? transaction = null);
        T GetById(int id, SqlConnection connection, SqlTransaction? transaction = null);
    }
}

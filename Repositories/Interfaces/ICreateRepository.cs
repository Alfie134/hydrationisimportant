using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICreateRepository<in T>
    {
        void Add(T entity, SqlConnection connection, SqlTransaction? transaction = null);
    }
}

using Microsoft.Data.SqlClient;

namespace Repositories.Interfaces.Base
{
    public interface IUpdateRepository<T>
    {
        void Update(T entity, SqlConnection connection, SqlTransaction? transaction = null);
    }
}

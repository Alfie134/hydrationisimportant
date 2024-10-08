using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces.Base;

namespace Repositories.Interfaces
{
    public interface IMunicipalityRepository : IReadRepository<Municipality>
    {
        public List<int> GetMunipalityByPostal(int postalCode, SqlConnection connection, SqlTransaction? transaction = null);
        public List<int> GetPostalsInMunipality(int MunipalityId, SqlConnection connection, SqlTransaction? transaction = null);
    }
}

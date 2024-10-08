using Configuration;
using Models;
using Microsoft.Data.SqlClient;
using Repositories.Interfaces;
using System.Transactions;

namespace Repositories
{
    public class MunicipalityRepository : IMunicipalityRepository
    {
        private readonly string _connectionString;

        public MunicipalityRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Municipality> GetAll(SqlConnection connection, SqlTransaction? transaction = null)
        {
            var municipalities = new List<Municipality>();
            string query = "SELECT * FROM Municipality";

            SqlCommand command = new SqlCommand(query, connection,transaction);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    municipalities.Add(new Municipality((int)reader["Id"], (string)reader["Name"], (int)reader["RegionId"]));
                }
            }
            return municipalities;
        }

        public Municipality GetById(int id, SqlConnection connection, SqlTransaction? transaction = null)
        {
            Municipality? municipality = null;
            string query = "SELECT * FROM Municipality WHERE Id = @Id";
        
            SqlCommand command = new SqlCommand(query, connection,transaction);
            command.Parameters.AddWithValue("Id", id);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    municipality = new Municipality((int)reader["Id"], (string)reader["Name"], (int)reader["RegionId"]);
                }
            }
            return municipality;
        }

        public List<int> GetMunipalityByPostal(int postalCode, SqlConnection connection, SqlTransaction? transaction = null)
        {
            List<int> PostalsInMunipality = new List<int>();

            string query = "SELECT MunicipalityId FROM Municipality_PostalCode WHERE PostalCode = @Id";

            SqlCommand command = new SqlCommand(query, connection, transaction);
            command.Parameters.AddWithValue("Id", postalCode);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    PostalsInMunipality.Add((int)reader["MunicipalityId"]);
                }
            }
            return PostalsInMunipality;
        }
        public List<int> GetPostalsInMunipality(int MunipalityId, SqlConnection connection, SqlTransaction? transaction = null)
        {
            List<int> PostalsInMunipality = new List<int>();

            string query = "SELECT PostalCode FROM Municipality_PostalCode WHERE MunicipalityId = @Id";

            SqlCommand command = new SqlCommand(query, connection, transaction);
            command.Parameters.AddWithValue("Id", MunipalityId);
            
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    PostalsInMunipality.Add((int)reader["PostalCode"]);
                }
            }
            return PostalsInMunipality;
        }
    }
}

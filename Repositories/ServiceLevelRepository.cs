using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class ServiceLevelRepository : IServiceLevelRepository
    {
        public ServiceLevelRepository()
        {
        }
        public IEnumerable<ServiceLevel> GetAll(SqlConnection connection, SqlTransaction? transaction = null)
        {
            var serviceLevels = new List<ServiceLevel>();
            string query = "SELECT * FROM ServiceLevel";

            SqlCommand command = new SqlCommand(query, connection, transaction);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    serviceLevels.Add(new ServiceLevel((int)reader["Id"], (string)reader["Name"], TimeSpan.FromMinutes((int)reader["TimeSpan"])));
                }
            }
            return serviceLevels;
        }

        public ServiceLevel GetById(int id, SqlConnection connection, SqlTransaction? transaction = null)
        {
            ServiceLevel? serviceLevel = null;
            string query = "SELECT * FROM ServiceLevel WHERE Id = @Id";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        serviceLevel = new ServiceLevel((int)reader["Id"], (string)reader["Name"], TimeSpan.FromMinutes((int)reader["TimeSpan"]));
                    }
                }
            }
            return serviceLevel;
        }

    }
}

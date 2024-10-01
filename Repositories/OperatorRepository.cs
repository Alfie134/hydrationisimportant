using Models;
using Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Configuration;

namespace Repositories
{
    public class OperatorRepository:IOperatorRepository
    {
        private readonly string _connectionString;

        public OperatorRepository()
        {
            _connectionString = new AppConfig().ConnectionString;
        }

        public IEnumerable<Operator> GetAll()
        {
            var operators = new List<Operator>();
            string query = "SELECT * FROM Operator";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        operators.Add(new Operator((int)reader["OperatorId"], (string)reader["Name"]));
                    }
                }
            }
            return operators;
        }

        public Operator GetById(int id)
        {
            Operator operatorr = null;
            string query = "SELECT * FROM Operator WHERE OperatorId = @OperatorId";

            using (SqlConnection connection = new SqlConnection(_connectionString)) 
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OperatorId", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        operatorr = new Operator((int)reader["OperatorId"], (string)reader["Name"]); 
                    }
                }
            }
            return operatorr;
        }
    }
}

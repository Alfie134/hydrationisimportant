using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Configuration;
using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class MissionRepository: IMissionRepository
    {
        private readonly string _connectionString = new AppConfig().ConnectionString;

        public int Add(Mission entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var command = new SqlCommand(@$"INSERT INTO Mission (Type,Description,RegionTaskId,ExpectedDeparture,Duration,ExpectedArrival,
                                        PatientName,RegionId,RouteId,FromPostal,ToPostal,ServiceLevelId,UserId) VALUES 
                                        (@Type,@Description,@RegionTaskId,@ExpectedDeparture,@Duration,@ExpectedArrival,@PatientName,@RegionId,@RouteId,@FromPostal,
                                        @ToPostal,@ServiceLevelId,@UserId); SELECT SCOPE_IDENTITY();", connection, transaction);

                    var id = 0;
                    using (command)
                    {
                        command.Parameters.AddWithValue("@Type", entity.Type);
                        command.Parameters.AddWithValue("@Description", entity.Description);
                        command.Parameters.AddWithValue("@RegionTaskId", entity.RegionalTaskId);
                        command.Parameters.AddWithValue("@ExpectedDeparture", entity.ExpectedDeparture);
                        command.Parameters.AddWithValue("@Duration", entity.DurationInMin);
                        command.Parameters.AddWithValue("@ExpectedArrival", entity.ExpectedArrival);
                        command.Parameters.AddWithValue("@PatientName", entity.PatientName);
                        command.Parameters.AddWithValue("@RegionId", entity.RegionId);
                        command.Parameters.AddWithValue("@FromPostal", entity.FromPostalCode);
                        command.Parameters.AddWithValue("@ToPostal", entity.ToPostalCode);
                        command.Parameters.AddWithValue("@ServiceLevelId", entity.ServiceLevelId);
                        // Handle RouteId being NULL
                        command.Parameters.AddWithValue("@RouteId", entity.RouteId.HasValue ? (object)entity.RouteId.Value : DBNull.Value);
                        // Handle UserId being NULL
                        command.Parameters.AddWithValue("@UserId", entity.UserId.HasValue ? (object)entity.UserId.Value : DBNull.Value);

                        id = Convert.ToInt32(command.ExecuteScalar());
                    }
                    transaction.Commit();
                    return id;
                }
            }
        }
        // DELETE 
        public bool Delete(Mission entity)
        {
            return DeleteById(entity.Id);
        }

        public bool DeleteById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var command = new SqlCommand("DELETE FROM Mission WHERE Id = @Id", connection, transaction);
                    using (command)
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
        }


        //READ
        public IEnumerable<Mission> GetAll()
        {
            var missions = new List<Mission>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open(); //lukker selv igen 
                using (var command = new SqlCommand("SELECT * FROM Mission", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Opret og tilføj en Mission direkte her
                        var mission = SerializeMission(reader);

                        missions.Add(mission); // Tilføj mission til listen
                    }
                }
            }
            return missions;
        }

        public Mission GetById(int id)
        {
            string query = "SELECT * FROM Mission WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return SerializeMission(reader);
                    }
                    else
                    {
                        throw new Exception("Could not find mission with Id");
                    }
                }
            }
        }

        public void Update(Mission entity)
        {
            throw new NotImplementedException();
        }

        private Mission SerializeMission(SqlDataReader reader)
        {
            return new Mission
            {
                //Skal vi nullable handle Id og RegionalTaskId ? :) 

                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Type = (TaskType)Enum.Parse(typeof(TaskType), reader.GetString(reader.GetOrdinal("Type"))),
                Description = reader.GetString(reader.GetOrdinal("Description")),
                RegionalTaskId = reader.GetString(reader.GetOrdinal("RegionTaskId")),
                ExpectedDeparture = reader.GetDateTime(reader.GetOrdinal("ExpectedDeparture")),
                DurationInMin = reader.GetInt32(reader.GetOrdinal("Duration")),
                ExpectedArrival = reader.GetDateTime(reader.GetOrdinal("ExpectedArrival")),
                PatientName = reader.GetString(reader.GetOrdinal("PatientName")),
                RegionId = reader.GetInt32(reader.GetOrdinal("RegionId")),
                FromPostalCode = reader.GetInt32(reader.GetOrdinal("FromPostal")),
                ToPostalCode = reader.GetInt32(reader.GetOrdinal("ToPostal")),
                ServiceLevelId = reader.GetInt32(reader.GetOrdinal("ServiceLevelId")),
                // Handle nullable værdier
                RouteId = reader.IsDBNull(reader.GetOrdinal("RouteId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("RouteId")),
                UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("UserId"))

                //Hvorfor bruges to- og fromaddress variablerne?
            };
        }
    }
}

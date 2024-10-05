using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class MissionRepository: IMissionRepository
    {
        private readonly string _connectionString;

        public MissionRepository(string connectionString) {
            _connectionString = connectionString;
        }

        public int Add(Mission entity, SqlConnection connection, SqlTransaction? transaction = null)
        {
            SqlCommand command = new SqlCommand(@$"INSERT INTO Mission (Type,Description,RegionalTaskId,ExpectedDeparture,Duration,ExpectedArrival,
                                        PatientName,RegionId,RouteId,FromPostal,ToPostal,ServiceLevelId,UserId) VALUES 
                                        (@Type,@Description,@RegionalTaskId,@ExpectedDeparture,@Duration,@ExpectedArrival,@PatientName,@RegionId,@RouteId,@FromPostal,
                                        @ToPostal,@ServiceLevelId,@UserId); SELECT SCOPE_IDENTITY();", connection, transaction);
            int id = 0;
            using (command)
            {
                command.Parameters.AddWithValue("@Type", entity.Type);
                command.Parameters.AddWithValue("@Description", entity.Description);
                command.Parameters.AddWithValue("@RegionalTaskId", entity.RegionalTaskId);
                command.Parameters.AddWithValue("@ExpectedDeparture", entity.ExpectedDeparture);
                command.Parameters.AddWithValue("@Duration", entity.DurationInMin);
                command.Parameters.AddWithValue("@ExpectedArrival", entity.ExpectedArrival);
                command.Parameters.AddWithValue("@PatientName", entity.PatientName);
                command.Parameters.AddWithValue("@RegionId", entity.RegionId);
                command.Parameters.AddWithValue("@FromPostal", entity.FromPostalCode);
                command.Parameters.AddWithValue("@ToPostal", entity.ToPostalCode);
                command.Parameters.AddWithValue("@ServiceLevelId", entity.ServiceLevelId);
                command.Parameters.AddWithValue("@RouteId", entity.RouteId.HasValue ? (object)entity.RouteId.Value : DBNull.Value);
                command.Parameters.AddWithValue("@UserId", entity.UserId.HasValue ? (object)entity.UserId.Value : DBNull.Value); 
                id = Convert.ToInt32(command.ExecuteScalar());
            }
            return id;
        }


        public IEnumerable<Mission> GetAll(SqlConnection connection, SqlTransaction? transaction = null)
        {
            var missions = new List<Mission>();
            string query = "SELECT * FROM Mission"; 
            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Mission mission = SerializeMission(reader);
                        missions.Add(mission);
                    }
                }
            }
            return missions;
        }

        public IEnumerable<Mission> GetMissionsByRouteId(int id,SqlConnection connection, SqlTransaction? transaction = null)
        {
            var missions = new List<Mission>();
            string query = "SELECT * FROM Mission WHERE RouteId = @id";


            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Mission mission = SerializeMission(reader);

                        missions.Add(mission);
                    }
                }
            }

            return missions;
        }

        public Mission GetById(int id, SqlConnection connection, SqlTransaction? transaction = null)
        {
            string query = "SELECT * FROM Mission WHERE Id = @Id";
          
            SqlCommand command = new SqlCommand(query, connection,transaction);
            command.Parameters.AddWithValue("@Id", id);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return SerializeMission(reader);
                }
                return null;
            }
        }

        public void Update(Mission entity, SqlConnection connection, SqlTransaction? transaction = null)
        {
            string query =
                "UPDATE Mission SET Type = @Type, Description = @Description, RegionalTaskId = @RegionalTaskId, ExpectedDeparture = @ExpectedDeparture, Duration = @Duration, ExpectedArrival = @ExpectedArrival, PatientName = @PatientName, RegionId = @RegionId, RouteId =@RouteId, FromPostal = @FromPostal, ToPostal = @ToPostal, ServiceLevelId = @ServiceLevelId, UserId = @UserId WHERE Id = @Id";
            
            SqlCommand command = new SqlCommand(query, connection, transaction);
            using (command)
            {
                command.Parameters.AddWithValue("@Type", entity.Type);
                command.Parameters.AddWithValue("@Description", entity.Description);
                command.Parameters.AddWithValue("@RegionalTaskId", entity.RegionalTaskId);
                command.Parameters.AddWithValue("@ExpectedDeparture", entity.ExpectedDeparture);
                command.Parameters.AddWithValue("@Duration", entity.DurationInMin);
                command.Parameters.AddWithValue("@ExpectedArrival", entity.ExpectedArrival);
                command.Parameters.AddWithValue("@PatientName", entity.PatientName);
                command.Parameters.AddWithValue("@RegionId", entity.RegionId);
                command.Parameters.AddWithValue("@FromPostal", entity.FromPostalCode);
                command.Parameters.AddWithValue("@ToPostal", entity.ToPostalCode);
                command.Parameters.AddWithValue("@ServiceLevelId", entity.ServiceLevelId);
                command.Parameters.AddWithValue("@RouteId", entity.RouteId.HasValue ? (object)entity.RouteId.Value : DBNull.Value);
                command.Parameters.AddWithValue("@UserId", entity.UserId.HasValue ? (object)entity.UserId.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Id", entity.Id);
                command.ExecuteNonQuery();
            }
        }

        public bool Delete(Mission entity, SqlConnection connection, SqlTransaction? transaction = null)
        {
           return DeleteById(entity.Id,connection,transaction);
        }

        public bool DeleteById(int id, SqlConnection connection, SqlTransaction? transaction = null)
        {
            var command = new SqlCommand("DELETE FROM Mission WHERE Id = @Id", connection, transaction);
            using (command)
            {
                command.Parameters.AddWithValue("@Id", id);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        private Mission SerializeMission(SqlDataReader reader)
        {
            return new Mission
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Type = (TaskType)reader.GetInt32(reader.GetOrdinal("Type")), // Hent som int og konverter til enum
                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                RegionalTaskId = reader.IsDBNull(reader.GetOrdinal("RegionalTaskId")) ? null : reader.GetString(reader.GetOrdinal("RegionalTaskId")),
                ExpectedDeparture = reader.GetDateTime(reader.GetOrdinal("ExpectedDeparture")),
                DurationInMin = reader.GetInt32(reader.GetOrdinal("Duration")),
                ExpectedArrival = reader.GetDateTime(reader.GetOrdinal("ExpectedArrival")),
                PatientName = reader.IsDBNull(reader.GetOrdinal("PatientName")) ? null : reader.GetString(reader.GetOrdinal("PatientName")),
                RegionId = reader.GetInt32(reader.GetOrdinal("RegionId")),
                FromPostalCode = reader.GetInt32(reader.GetOrdinal("FromPostal")),
                ToPostalCode = reader.GetInt32(reader.GetOrdinal("ToPostal")),
                ServiceLevelId = reader.GetInt32(reader.GetOrdinal("ServiceLevelId")),
                RouteId = reader.IsDBNull(reader.GetOrdinal("RouteId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("RouteId")),
                UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("UserId"))
            };
        }
    }
}

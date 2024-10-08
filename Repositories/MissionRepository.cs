using Microsoft.Data.SqlClient;
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
            SqlCommand command = new SqlCommand(@$"INSERT INTO Mission (RegionId,RegionalTaskId,Type,Description,ServiceLevelId,ExpectedDeparture,Duration,ExpectedArrival,
                                        FromAddress,FromPostal,ToAddress,ToPostal,PatientName,RouteId,UserId) VALUES 
                                        (@RegionId,@RegionalTaskId,@Type,@Description,@ServiceLevelId,@ExpectedDeparture,@Duration,@ExpectedArrival,
                                        @FromAddress,@FromPostal,@ToAddress,@ToPostal,@PatientName,@RouteId,@UserId); SELECT SCOPE_IDENTITY();", connection, transaction);
            int id = 0;
            using (command)
            {
                command.Parameters.AddWithValue("@RegionId", entity.RegionId);
                command.Parameters.AddWithValue("@RegionalTaskId", entity.RegionalTaskId);
                command.Parameters.AddWithValue("@Type", entity.Type);
                command.Parameters.AddWithValue("@Description", entity.Description);
                command.Parameters.AddWithValue("@ExpectedDeparture", entity.ExpectedDeparture);
                command.Parameters.AddWithValue("@Duration", entity.DurationInMin);
                command.Parameters.AddWithValue("@ExpectedArrival", entity.ExpectedArrival);
                command.Parameters.AddWithValue("@FromAddress", entity.FromAddress);
                command.Parameters.AddWithValue("@FromPostal", entity.FromPostalCode);
                command.Parameters.AddWithValue("@ToAddress", entity.ToAddress);
                command.Parameters.AddWithValue("@ToPostal", entity.ToPostalCode);
                command.Parameters.AddWithValue("@ServiceLevelId", entity.ServiceLevelId);
                command.Parameters.AddWithValue("@RouteId", entity.RouteId.HasValue ? entity.RouteId.Value : DBNull.Value);
                command.Parameters.AddWithValue("@AssignedVehicle", entity.AssignedVehicle != null? entity.AssignedVehicle.Id : DBNull.Value);
                command.Parameters.AddWithValue("@UserId", entity.UserId.HasValue ? entity.UserId.Value : DBNull.Value);
                command.Parameters.AddWithValue("@PatientName", entity.PatientName);
                id = Convert.ToInt32(command.ExecuteScalar());
            }
            return id;
        }
        //Henter ALLE missioner
            public IEnumerable<Mission> GetAll(SqlConnection connection, SqlTransaction? transaction = null)
        {
            var missions = new List<Mission>();
            string query = @"SELECT Mission.Id, Mission.RegionId, Mission.ServiceLevelId, Mission.RouteId, Mission.UserId, Mission.RegionalTaskId, Mission.Type, ServiceLevel.Name, 
                            Mission.ExpectedDeparture, Mission.Duration, Mission.ExpectedArrival, 
                            FromPostalT.PostalCode AS FromPostal, Mission.FromAddress, 
                            ToPostalT.PostalCode AS ToPostal, Mission.ToAddress, 
                            Mission.PatientName, Mission.Description, ServiceLevel.TimeSpan

                            FROM Mission
                            JOIN PostalCode AS FromPostalT ON Mission.FromPostal = FromPostalT.PostalCode
                            JOIN PostalCode AS ToPostalT ON Mission.ToPostal = ToPostalT.PostalCode
                            JOIN ServiceLevel ON Mission.ServiceLevelId = ServiceLevel.Id;";
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

        //Finder alle opgaver på en rute
        public IEnumerable<Mission> GetMissionsByRouteId(int id,SqlConnection connection, SqlTransaction? transaction = null)
        {
            var missions = new List<Mission>();
            string query = @"SELECT Mission.*, ServiceLevel.Name, ServiceLevel.Time FROM Mission JOIN ServiceLevel ON Mission.ServiceLevelId = ServiceLevel.Id
                WHERE RouteId = @Id";


            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@Id", id);

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
        //filterer opgaver efter dato og om de er tildelt en rute
        public IEnumerable<Mission> GetFilteredMissions(DateTime? selectedDate, bool showAllMissions, SqlConnection connection, SqlTransaction? transaction = null)
        {
            List<Mission> missions = new List<Mission>();
            string query = @" SELECT Mission.*, ServiceLevel.Name, ServiceLevel.Time FROM Mission JOIN ServiceLevel ON Mission.ServiceLevelId = ServiceLevel.Id
                    WHERE (@SelectedDate IS NULL OR CONVERT(date, ExpectedDeparture) = CONVERT(date, @SelectedDate))
                    AND (@ShowAllMissions = 1 OR RouteId IS NULL)";


            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@SelectedDate", (object)selectedDate ?? DBNull.Value);
                command.Parameters.AddWithValue("@ShowAllMissions", showAllMissions ? 1 : 0);

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


        public List<Mission> SuggestMissionsByPostals(DateTime dateTime, List<int> postals, bool isItArrival, SqlConnection connection, SqlTransaction transaction)
        {
            var missions = new List<Mission>();

            string query = "";
            var postalParams = string.Join(",", postals);

            if (isItArrival == false)
            {
                query = $@"
                SELECT * FROM Mission
                WHERE CAST(ExpectedDeparture AS DATE) = @DateTime AND (ToPostal IN ({postalParams}))";
            }
            else
            {
                query = $@"
                SELECT * FROM Mission
                WHERE CAST(ExpectedDeparture AS DATE) = @DateTime AND (FromPostal IN ({postalParams}))";

            }

            using (var command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@DateTime", dateTime.Date); // Konverter til dato uden tid
                using (var reader = command.ExecuteReader())
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

        //Henter mission med et bestemt id
        public Mission GetById(int id, SqlConnection connection, SqlTransaction? transaction = null)
        {
            string query = @"SELECT Mission.*, ServiceLevel.Name, ServiceLevel.TimeSpan 
            FROM Mission 
            JOIN ServiceLevel ON Mission.ServiceLevelId = ServiceLevel.Id 
            WHERE Mission.Id = @Id;";
          
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
                @"UPDATE Mission SET
                Type = @Type, Description = @Description,
                RegionalTaskId = @RegionalTaskId, ExpectedDeparture = @ExpectedDeparture,
                Duration = @Duration, ExpectedArrival = @ExpectedArrival, PatientName = @PatientName,
                RegionId = @RegionId, RouteId = @RouteId, FromAddress = @FromAddress, FromPostal = @FromPostal,
                ToAddress = @ToAddress, ToPostal = @ToPostal, ServiceLevelId = @ServiceLevelId,
                UserId = @UserId 
                WHERE Mission.Id = @Id";

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
                command.Parameters.AddWithValue("@FromAddress", entity.FromAddress);
                command.Parameters.AddWithValue("@FromPostal", entity.FromPostalCode);
                command.Parameters.AddWithValue("@ToAddress", entity.ToAddress);
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
                FromAddress = reader.IsDBNull(reader.GetOrdinal("FromAddress")) ? null : reader.GetString(reader.GetOrdinal("FromAddress")),
                FromPostalCode = reader.GetInt32(reader.GetOrdinal("FromPostal")),
                ToAddress = reader.IsDBNull(reader.GetOrdinal("ToAddress")) ? null : reader.GetString(reader.GetOrdinal("ToAddress")),
                ToPostalCode = reader.GetInt32(reader.GetOrdinal("ToPostal")),
                ServiceLevelId = reader.GetInt32(reader.GetOrdinal("ServiceLevelId")),
                ServiceLevel = new ServiceLevel(reader.GetInt32(reader.GetOrdinal("ServiceLevelId")), reader.GetString(reader.GetOrdinal("Name")), new TimeSpan(0, reader.GetInt32(reader.GetOrdinal("TimeSpan")), 0)),
                RouteId = reader.IsDBNull(reader.GetOrdinal("RouteId")) ? null : reader.GetInt32(reader.GetOrdinal("RouteId")),
                UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? null : reader.GetInt32(reader.GetOrdinal("UserId"))
            };
        }
    }
}

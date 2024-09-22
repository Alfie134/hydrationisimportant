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
    public class MissionRepository: IMissionRepository
    {
        public int Add(Mission entity, SqlConnection connection, SqlTransaction? transaction = null)
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
            return id;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Mission> GetAll()
        {
            throw new NotImplementedException();
        }

        public Mission GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Mission entity)
        {
            throw new NotImplementedException();
        }
    }
}

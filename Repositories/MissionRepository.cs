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
    public class MissionRepository: IRepository<Mission>
    {

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

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

//        public void Add(Mission entity, SqlConnection connection, SqlTransaction? transaction = null)
//        {
//           throw new NotImplementedException();
//        }

        public void Add(Mission entity)
        {
            var command = new SqlCommand(@$"INSERT INTO Mission (Type,Description,RegionTaskId,ExpectedDeparture,Duration,ExpectedArrival,
PatientName,RegionId,RouteId,FromPostal,ToPostal,ServiceLevelId,UserId) VALUES 
(@Type,@Description,@RegionTaskId,@ExpectedDeparture,@Duration,@ExpectedArrival,@PatientName,@RegionId,@RouteId,@FromPostal,
@ToPostal,@ServiceLevelId,@UserId)", connection, transaction);

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
                // Handle RouteId being NULL
                if (entity.RouteId.HasValue)
                {
                    command.Parameters.AddWithValue("@RouteId", entity.RouteId.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@RouteId", DBNull.Value);
                }
                command.Parameters.AddWithValue("@FromPostal", entity.FromPostalCode);
                command.Parameters.AddWithValue("@ToPostal", entity.ToPostalCode);
                command.Parameters.AddWithValue("@ServiceLevelId", entity.ServiceLevelId);
                // Handle UserId being NULL
                if (entity.UserId.HasValue)
                {
                    command.Parameters.AddWithValue("@UserId", entity.UserId.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@UserId", DBNull.Value);
                }
               
                command.ExecuteNonQuery();
            }
        }
    }
}

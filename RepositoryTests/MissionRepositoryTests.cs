using Configuration;
using Microsoft.Data.SqlClient;
using Models;
using Repositories;
using Repositories.Interfaces;

namespace RepositoryTests
{
    [TestClass]
    public class MissionRepositoryTests
    {
        private IMissionRepository _missionRepository = new MissionRepository();

        private readonly string _connectionString = new AppConfig().ConnectionString;


        [TestMethod]
        public void CreateMission()
        {

            //Arrange
            Mission mission = new Mission()
            {
                RegionalTaskId = "TestId",
                ExpectedDeparture = DateTime.Now,
                DurationInMin = 120,
                ExpectedArrival = DateTime.Now.AddMinutes(120),
                PatientName = "Jane Doe",
                RouteId = null,
                FromPostalCode = 8940,
                ToPostalCode = 8900,
                Type = TaskType.C,
                ServiceLevelId = 1,
                RegionId = 1082,

                Description = "Mission 1 Description",
            };

            //Act
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Pass the same connection and transaction to both repositories
                        _missionRepository.Add(mission, connection, transaction);
                        // Commit the transaction if both operations succeed
                        transaction.Commit();
                    }
                    catch
                    {
                        // Rollback the transaction if any operation fails
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            //Assert
        }
    }
}
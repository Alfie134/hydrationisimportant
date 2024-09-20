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
        /*
        private IMissionRepository _missionRepository = new MissionRepository();

        private readonly string _connectionString = new AppConfig().ConnectionString;


        [TestMethod]
        public void CreateMission()
        {


            Mission mission = new Mission()
            {
                RegionId = 1,
                ExpectedDeparture = DateTime.Now,
                DurationInMin = 120,
                Type = MissionType.C,

                
                Description = "Mission 1 Description",
            };

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
        }
        */
    }
}
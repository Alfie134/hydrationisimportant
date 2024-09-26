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
        // ------------ Jeg havde fejl så jeg prøver lige udkommentere og bytte med nedenstående kode -----------

        // private IMissionRepository _missionRepository = new MissionRepository(_connectionString);
        // private readonly string _connectionString = new AppConfig().ConnectionString;

        // ------------ Jeg havde fejl så jeg prøver lige udkommentere ovenstående og bytte med nedenstående kode -----------

        // Connection string defineres som readonly og initialiseres i konstruktøren
        private readonly string _connectionString;
        private readonly IMissionRepository _missionRepository;

        public MissionRepositoryTests()
        {
            // Initialisering af connection string fra AppConfig
            _connectionString = new AppConfig().ConnectionString;

            // Initialisering af MissionRepository med connection string
            _missionRepository = new MissionRepository(_connectionString);
        }


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
            int id;
            //Act
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Pass the same connection and transaction to both repositories
                        id = _missionRepository.Add(mission, connection, transaction);
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
            Assert.IsTrue(id > 0);
        }

    }
}
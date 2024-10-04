using Configuration;
using Microsoft.Data.SqlClient;
using Models;
using Repositories;
using Repositories.Interfaces;

namespace RepositoryTests
{
    [TestClass]
    public class RouteRepositoryTest: RepositoryTestBase
    {
        private string _connectionString;
        private IMissionRepository _missionRepository;
        private IRouteRepository _routeRepository;

        [TestInitialize]
        public void Setup()
        {
            _connectionString = ConnectionString;
            _missionRepository = new MissionRepository(_connectionString);
            _routeRepository = new RouteRepository(_connectionString);
        }

        [TestMethod]
        public void Test_CreateRouteWithMissions()
        {
            // Arrange
            int vehicleId = 4; // Indsæt en eksisterende VehicleId
            Route route = new Route(vehicleId);

            // Opret nogle missioner
            var mission1 = new Mission
            {
                RegionId = 1082,
                RegionalTaskId = "Task001",
                Type = TaskType.C, // Bruger enum til type C
                Description = "Transport patient A",
                ServiceLevelId = 1,
                ExpectedDeparture = DateTime.Now.AddMinutes(10),
                DurationInMin = 30,
                ExpectedArrival = DateTime.Now.AddMinutes(40),
                FromAddress = "Helgenæsvej 5",
                FromPostalCode = 8940,
                ToAddress = "Skovlyvej 15",
                ToPostalCode = 8900,
                PatientName = "Jane Doe"
            };

            var mission2 = new Mission
            {
                RegionId = 1083,
                RegionalTaskId = "Task002",
                Type = TaskType.D, // Bruger enum til type D
                Description = "Transport patient B",
                ServiceLevelId = 1,
                ExpectedDeparture = DateTime.Now.AddMinutes(20),
                DurationInMin = 45,
                ExpectedArrival = DateTime.Now.AddMinutes(65),
                FromAddress = "Skovlyvej 15",
                FromPostalCode = 8900,
                ToAddress = "Beriderbakken 4",
                ToPostalCode = 7100,
                PatientName = "Patient B"
            };

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Opret ruten
                        int routeId = _routeRepository.Add(route, connection, transaction);
                        route.Id = routeId;

                        // Opret missionerne og knyt dem til ruten
                        mission1.RouteId = routeId; // Sætte RouteId på missionen
                        mission2.RouteId = routeId; // Sætte RouteId på missionen

                        _missionRepository.Add(mission1, connection, transaction);
                        _missionRepository.Add(mission2, connection, transaction);

                        // Commit transactionen
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                // Act
                var missions = _missionRepository.GetMissionsByRouteId(route.Id, connection);

                // Assert
                Assert.IsNotNull(missions, "Missions should not be null.");
                Assert.AreEqual(2, missions.Count(), "The route should have exactly 2 missions.");
                Assert.AreEqual(mission1.Description, missions.First(m => m.RegionalTaskId == "Task001").Description, "Mission 1 description does not match.");
                Assert.AreEqual(mission2.Description, missions.First(m => m.RegionalTaskId == "Task002").Description, "Mission 2 description does not match.");
                Assert.AreEqual(mission1.Type, missions.First(m => m.RegionalTaskId == "Task001").Type, "Mission 1 type does not match.");
                Assert.AreEqual(mission2.Type, missions.First(m => m.RegionalTaskId == "Task002").Type, "Mission 2 type does not match.");
            }
        }


    }
}

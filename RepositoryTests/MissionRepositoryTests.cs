using Microsoft.Data.SqlClient;
using Models;
using Repositories;
using Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bogus;

namespace RepositoryTests
{
    [TestClass]
    public class MissionRepositoryTests: RepositoryTestBase
    {
        private string _connectionString;
        private IMissionRepository _missionRepository;

        [TestInitialize]
        public void Setup()
        {
            _connectionString = ConnectionString;
            _missionRepository = new MissionRepository(_connectionString);
        }

        [TestMethod]
        public void CreateMission()
        {
            // Arrange
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

            // Act
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        id = _missionRepository.Add(mission, connection, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            // Assert
            Assert.IsTrue(id > 0);
        }

        [TestMethod]
        public void Test_GetAll_ReturnsListOfMissions()
        {
            // Arrange
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Act
                var missions = _missionRepository.GetAll(connection);

                // Assert
                Assert.IsNotNull(missions, "The result should not be null.");
                Assert.IsTrue(missions.Count() > 0, "The result should contain at least one mission.");
            }
        }

        [TestMethod]
        public void GetById_ShouldReturnCorrectMission()
        {
            // Arrange
            Mission mission = new Mission()
            {
                RegionalTaskId = "TestId",
                ExpectedDeparture = DateTime.Now,
                DurationInMin = 120,
                ExpectedArrival = DateTime.Now.AddMinutes(120),
                PatientName = "John Doe",
                RouteId = null,
                FromPostalCode = 8940,
                ToPostalCode = 8900,
                Type = TaskType.C,
                ServiceLevelId = 1,
                RegionId = 1082,
                Description = "Test mission for GetById",
            };
            int id;

            // Act: Create a mission first
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        id = _missionRepository.Add(mission, connection, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            // Act: Retrieve the mission by its Id
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var retrievedMission = _missionRepository.GetById(id, connection);

                // Assert
                Assert.IsNotNull(retrievedMission);
                Assert.AreEqual(mission.RegionalTaskId, retrievedMission.RegionalTaskId);
                Assert.AreEqual(mission.Description, retrievedMission.Description);
                Assert.AreEqual(mission.PatientName, retrievedMission.PatientName);
            }
        }

        [TestMethod]
        public void UpdateMission_ShouldUpdateCorrectly()
        {
            // Arrange
            Mission mission = new Mission()
            {
                RegionalTaskId = "InitialId",
                ExpectedDeparture = DateTime.Now,
                DurationInMin = 120,
                ExpectedArrival = DateTime.Now.AddMinutes(120),
                PatientName = "John Doe",
                RouteId = null,
                FromPostalCode = 8940,
                ToPostalCode = 8900,
                Type = TaskType.C,
                ServiceLevelId = 1,
                RegionId = 1082,
                Description = "Initial Description",
            };

            int id;

            // First, we add a mission to update
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        id = _missionRepository.Add(mission, connection, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            // Modify the mission with new values
            mission.Id = id;
            mission.Description = "Updated Description";
            mission.Type = TaskType.D;
            mission.PatientName = "Jane Doe";

            // Act: Update the mission
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        _missionRepository.Update(mission, connection, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            // Assert: Verify the mission was updated correctly
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var updatedMission = _missionRepository.GetById(id, connection);

                Assert.IsNotNull(updatedMission);
                Assert.AreEqual("Updated Description", updatedMission.Description);
                Assert.AreEqual(TaskType.D, updatedMission.Type);
                Assert.AreEqual("Jane Doe", updatedMission.PatientName);
            }
        }

        [TestMethod]
        public void DeleteMission_ShouldRemoveMission()
        {
            // Arrange
            Mission mission = new Mission()
            {
                RegionalTaskId = "DeleteId",
                ExpectedDeparture = DateTime.Now,
                DurationInMin = 120,
                ExpectedArrival = DateTime.Now.AddMinutes(120),
                PatientName = "Mark Doe",
                RouteId = null,
                FromPostalCode = 8940,
                ToPostalCode = 8900,
                Type = TaskType.C,
                ServiceLevelId = 1,
                RegionId = 1082,
                Description = "Mission for delete test",
            };
            int id;

            // Act: Create a mission first
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        id = _missionRepository.Add(mission, connection, transaction);
                        mission.Id = id;
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            // Act: Delete the mission
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        _missionRepository.Delete(mission, connection, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            // Assert: Verify the mission was deleted
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var deletedMission = _missionRepository.GetById(id, connection);

                Assert.IsNull(deletedMission, "The mission should be deleted and thus be null.");
            }
        }


        [TestMethod]
        public void CreateRandomMissions()
        {
            IRegionRepository regionRepository = new RegionRepository(_connectionString);
            IMunicipalityRepository municipalityRepository = new MunicipalityRepository(_connectionString);
            IVehicleRepository vehicleRepository = new VehicleRepository();
            IServiceLevelRepository serviceLevelRepository = new ServiceLevelRepository();
            IPostalRepository postalRepository = new PostalRepository(_connectionString);
        
            List<Mission> missionsList = new List<Mission>();

            List<Municipality> municipalityList = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        municipalityList = municipalityRepository.GetAll(connection, transaction).ToList();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            List<Vehicle> vehiclesList = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        vehiclesList = vehicleRepository.GetAll(connection, transaction).ToList();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            List<ServiceLevel> serviceLevelList = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        serviceLevelList = serviceLevelRepository.GetAll(connection, transaction).ToList();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            List<Region> regionsList = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        regionsList = regionRepository.GetAll(connection, transaction).ToList();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            List<Postal> ppostalsList = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        ppostalsList = postalRepository.GetAll(connection, transaction).ToList();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            var testmission = new Faker<Mission>()
                .RuleFor(o => o.RegionId, f => f.PickRandom(regionsList.Select(x => x.RegionId)))
                .RuleFor(o => o.RegionalTaskId, f => "RegionTaskId")
                .RuleFor(o => o.Type, f => TaskType.D)
                .RuleFor(o => o.Description, f => "Random Description 5.0")
                .RuleFor(o => o.ServiceLevelId, f => f.PickRandom(serviceLevelList.Select(x => x.Id)))
                .RuleFor(o => o.ExpectedDeparture, f => DateTime.Now)
                .RuleFor(o => o.DurationInMin, f => f.Random.Number(60, 240))
                .RuleFor(o => o.ExpectedArrival, f => DateTime.Now.AddMinutes(f.Random.Number(60, 240)))
                .RuleFor(o => o.AssignedVehicle, f => new Vehicle())
                .RuleFor(O => O.FromAddress, f => $"{f.Address.City()} {f.Address.StreetName()} {f.Address.SecondaryAddress()}")
                .RuleFor(o => o.FromPostalCode, f => f.PickRandom(ppostalsList.Select(p => p.PostalNumber)))
                .RuleFor(o => o.ToAddress, f => $"{f.Address.City()} {f.Address.StreetName()} {f.Address.SecondaryAddress()}")
                .RuleFor(o => o.ToPostalCode, f => f.PickRandom(ppostalsList.Select(p => p.PostalNumber)))
                .RuleFor(o => o.PatientName, f => $"{f.Name.LastName()} {f.Name.FirstName()}")
                .RuleFor(o => o.RouteId, f => f.Random.Number(1, 21))
                .RuleFor(o => o.UserId, f => f.Random.Number(1, 21));

                missionsList = testmission.Generate(20).ToList();
                using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    { 
                        foreach (Mission mission in missionsList){
                    
                        _missionRepository.Add(mission, connection, transaction);
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}

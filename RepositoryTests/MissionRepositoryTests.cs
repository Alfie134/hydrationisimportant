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
        public TestContext TestContext { get; set; }
        private IMissionRepository _missionRepository;

        [TestInitialize]
        public void InitializeTest()
        {
            _missionRepository = new MissionRepository((string)TestContext.Properties["testdatabaseURL"]);
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
            int id = _missionRepository.Add(mission); //Hver gang metoden testes, tilføjes ny data i databasen

            //Assert
            Assert.IsTrue(id > 0);
        }

        [TestMethod]
        public void GetAll_ReturnsAllRows()
        {
            IEnumerable<Mission> missionsList = _missionRepository.GetAll();
            Assert.IsTrue(missionsList.Count() != 0);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectInfo()
        {
            Mission tempMission = _missionRepository.GetById(3);
//            Assert.IsNotNull(tempMission);   
//            Assert.IsTrue(tempMission.Id == "Facility");
        }
    }
}   
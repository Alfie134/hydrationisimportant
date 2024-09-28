using Models;
using Repositories;
using Repositories.Interfaces;

namespace RepositoryTests
{
    [TestClass]
    public class OperatorRepositoryTests
    {
        public TestContext TestContext { get; set; }
        private IOperatorRepository _operatorRepository;

        [TestInitialize]
        public void InitializeTest() 
        {
            _operatorRepository = new OperatorRepository((string)TestContext.Properties["testdatabaseURL"]);
        }

        [TestMethod]
        public void GetAll_ReturnAllRows()
        {
            IEnumerable<Operator> operatorsList = _operatorRepository.GetAll();
            Assert.IsTrue(operatorsList.Count()!= 0);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectInfo()
        {
            Operator tempOperator = _operatorRepository.GetById(1);
            Assert.IsNotNull(tempOperator);
            Assert.IsTrue(tempOperator.Name == "Bent");
        }
    }
}

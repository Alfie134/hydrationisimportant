using Models;
using Repositories;
using Repositories.Interfaces;

namespace RepositoryTests
{
    [TestClass]
    public class UserRepositoryTests
    {
        public TestContext TestContext { get; set; }
        private IUserRepository _userRepository;

        [TestInitialize]
        public void InitializeTest()
        {
            _userRepository = new UserRepository((string)TestContext.Properties["testdatabaseURL"]);
        }

        [TestMethod]
        public void GetAll_ReturnAllRows()
        {
            IEnumerable<User> usersList = _userRepository.GetAll();
            Assert.IsTrue(usersList.Count() != 0);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectInfo()
        {
            User tempUser = _userRepository.GetById(1);
            Assert.IsNotNull(tempUser);
            Assert.IsNotNull(tempUser.Password);
            Assert.IsTrue(tempUser.UserName == "Bente");
        }
    }
}

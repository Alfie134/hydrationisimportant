using Repositories;
using Repositories.Interfaces;
using Models;
using Services;

namespace RepositoryTests.ServiceTests
{
    [TestClass]
    public class UserServiceTests
    {
        public TestContext TestContext { get; set; }
        private UserService _userService;

        [TestInitialize]
        public void InitializeTest()
        {
            _userService = new UserService();
        }

        [TestMethod]
        public void UserLogin_ReturnsUserSucessfulLogin()
        {
            User tempUser = _userService.UserLogin("Bente", "1234");
            Assert.IsNotNull(tempUser);
            Assert.IsTrue(tempUser.Id == 1);
        }

        [TestMethod]
        public void UserLogin_UnsucessfulLogin()
        {
            User tempUser = _userService.UserLogin("", "");
            Assert.IsNull(tempUser);
        }
    }
}

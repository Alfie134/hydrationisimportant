using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Services;

namespace ServiceTests
{
    [TestClass]
    public class UserServiceTests: ServiceTestBase
    {
        private readonly UserService _userService;
        public UserServiceTests()
        {
            _userService = new UserService();
        }
        [TestMethod]
        public void Create_user_with_hashed_password()
        {

            var result = _userService.CreateUser("testuser", "123456", 1082);
            Assert.IsTrue(result.IsSuccess);

        }


    }
}

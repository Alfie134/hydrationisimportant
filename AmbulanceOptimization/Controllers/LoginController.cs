using System.Security;
using Models;
using Services;

namespace AmbulanceOptimization.Controllers
{
    class LoginController
    {
        private readonly UserService _userService;

        public LoginController()
        {
            _userService = new UserService();
        }
        public User AuthenticateUser(string UserName, SecureString password)
        {
            var unsecurePassword = ConvertToUnsecureString(password);
           return _userService.UserLogin(UserName,unsecurePassword);
        }
        private string ConvertToUnsecureString(SecureString securePassword)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return System.Runtime.InteropServices.Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}

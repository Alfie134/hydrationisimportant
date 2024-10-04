using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Configuration;

namespace RepositoryTests
{
    public class RepositoryTestBase
    {
        protected string ConnectionString;

        public RepositoryTestBase()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
            ConnectionString = new AppConfig().ConnectionString;
        }
    }
}

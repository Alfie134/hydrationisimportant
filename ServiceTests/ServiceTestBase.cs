using Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTests
{
    public abstract class ServiceTestBase
    {
        protected string ConnectionString;

        protected ServiceTestBase()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
            ConnectionString = new AppConfig().ConnectionString;
        }
    }
}

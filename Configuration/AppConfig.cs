using Microsoft.Extensions.Configuration;
using System;

namespace Configuration
{
    public class AppConfig
    {
        private IConfigurationRoot _configuration;

        public AppConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }
        public string ConnectionString {

            get
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                if (environment == "Test")
                {
                    return _configuration.GetConnectionString("TestConnection");
                }
                else
                {
                    return _configuration.GetConnectionString("DefaultConnection");
                }
            }
        } 
        public string PasswordPepper => _configuration["AppSettings:PasswordPepper"];
    }
}

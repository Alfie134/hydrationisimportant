using Microsoft.Extensions.Configuration;

namespace Configuration
{
    public class AppConfig
    {
        private IConfigurationRoot _configuration;

        public AppConfig()
        {
            // Initialize configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  // You may need to adjust this path
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        // Expose Connection String
        public string ConnectionString => _configuration.GetConnectionString("DefaultConnection");

    }
}

using Configuration;
using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MunicipalityService
    {
        private readonly string _connectionString;
        private readonly IMunicipalityRepository _municipalityRepository;
        public MunicipalityService()
        {
            var config = new AppConfig();
            _connectionString = config.ConnectionString;
            _municipalityRepository = new MunicipalityRepository(_connectionString);
        }
    }
}
 
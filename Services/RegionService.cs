using Configuration;
using Repositories.Interfaces;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Models;
using Microsoft.Identity.Client;

namespace Services
{
    public class RegionService
    {
        private readonly string _connectionString;
        private readonly IRegionRepository _regionRepository;
        public RegionService()
        {
            var config = new AppConfig();
            _connectionString = config.ConnectionString;
            _regionRepository = new RegionRepository(_connectionString);
        }

        public List<Region> GetAll()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            using (connection)
            {
                connection.Open();
                return (List<Region>)_regionRepository.GetAll(connection);
            }
        }
    }
}


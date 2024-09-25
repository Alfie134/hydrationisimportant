using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Configuration;
using Microsoft.Data.SqlClient;
using Models;
using Repositories;
using Repositories.Interfaces;

namespace Services
{
    public class MissionService
    {

        //private readonly string _connectionString;
        //private readonly IMissionRepository _missionRepository;
        //public MissionService()
        //{
        //    var config = new AppConfig();
        //    _connectionString = config.ConnectionString;
        //    _missionRepository = new MissionRepository();

        //}
        //public void AddMission(Mission mission)
        //{

        //     Add mission to database
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        connection.Open();
        //        using (var transaction = connection.BeginTransaction())
        //        {
        //            try
        //            {
        //                 Pass the same connection and transaction to both repositories
        //                _missionRepository.Add(mission, connection, transaction);
        //                 Commit the transaction if both operations succeed
        //                transaction.Commit();
        //            }
        //            catch
        //            {
        //                 Rollback the transaction if any operation fails
        //                transaction.Rollback();
        //                throw;
        //            }
        //        }
        //    }
        //}
        
    }
}

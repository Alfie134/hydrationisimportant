using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private readonly string _connectionString;
        private readonly IMissionRepository _missionRepository;
        public MissionService()
        {
            var config = new AppConfig();
            _connectionString = config.ConnectionString;
            _missionRepository = new MissionRepository(_connectionString);

        }
        public void AddMission(Mission mission)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        _missionRepository.Add(mission, connection, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<Mission> GetAll()
        {
            List<Mission> tempMissions = new List<Mission>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        tempMissions = (List<Mission>)_missionRepository.GetAll( connection, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return tempMissions;
        }
        public List<Mission> GetFilteredMissions(DateTime? dateTime, bool AllMissions)
        {
            List<Mission> tempMissions = new List<Mission>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        tempMissions = (List<Mission>)_missionRepository.GetFilteredMissions(dateTime,AllMissions, connection, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return tempMissions;
        }
        public List<Mission> GetMissionsByRouteId(int RouteId)
        {
            List<Mission> tempMissions = new List<Mission>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        tempMissions = (List<Mission>)_missionRepository.GetMissionsByRouteId(RouteId, connection, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return tempMissions;
        }
    }
}

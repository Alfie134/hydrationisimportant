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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services
{
    public class SuggestRouteService
    {
        private readonly string _connectionString;
        private readonly IMissionRepository _missionRepository;
        private readonly IMunicipalityRepository _municipalityRepository;
        public SuggestRouteService()
        {
            var config = new AppConfig();
            _connectionString = config.ConnectionString;
            _missionRepository = new MissionRepository(_connectionString);
            _municipalityRepository = new MunicipalityRepository(_connectionString);

        }

        public List<Mission> SuggestOnMunicality(DateTime date, int postal, bool arrival)
        {
           //du kommer fra foreach loop med alle de missioner der er tilføjet og de har et postal(int) og en dato

           //du tager nu det postnummer og skal først finde udad hvilken kommune postnummeret ligger i (det kan være flere) 
           List<int> municipalitiIdes = GetMunicaliFromPostal(postal);

           //så skal du finde udad hvilke andre postnumre der er i kommunen
           List<int> AllPostalsInMunipality = new List<int>();
           foreach (int municipaltlityId in municipalitiIdes)
           {
               AllPostalsInMunipality.AddRange(GetAllPostalsInMunicality(municipaltlityId));
           }

           //og nu kan du lave et foreach loop der tjekker samtlige postnumre i kommunen 
           List<Mission> missionsInMunicipality = new List<Mission>();
           missionsInMunicipality.AddRange(SuggestMissionsByPostals(date, AllPostalsInMunipality,arrival));
           return missionsInMunicipality;
        }

        public List<Mission> SuggestMissionsByPostals(DateTime date, List<int> postals, bool arrival)
        {
            List<Mission> tempMissions = new List<Mission>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        tempMissions = (List<Mission>)_missionRepository.SuggestMissionsByPostals(date, postals, arrival, connection, transaction);
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

        private List<int> GetMunicaliFromPostal(int postalCode)
        {
            {
                List<int> Postals = new List<int>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            Postals = (List<int>)_municipalityRepository.GetMunipalityByPostal(postalCode, connection, transaction);
                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
                return Postals;
            }
        }
        private List<int> GetAllPostalsInMunicality(int MunipalityId)
        {
            {
                List<int> Postals = new List<int>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            Postals = (List<int>)_municipalityRepository.GetPostalsInMunipality(MunipalityId, connection, transaction);
                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
                return Postals;
            }
        }
    }
}

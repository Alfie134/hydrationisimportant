using Configuration;
using Microsoft.Data.SqlClient;
using Models;
using Repositories;
using Repositories.Interfaces;

namespace RepositoryTests
{
    [TestClass]
    public class PostalRepositoryTests
    {
        
        private IPostalRepository _postalRepository = new PostalRepository();


        [TestMethod]
        public void GetAll_ReturnsAllRows()
        {
           IEnumerable<Postal> postalsList = _postalRepository.GetAll();
            Assert.IsTrue(postalsList.Count() != 0);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectInfo()
        {
            Postal tempPostal = _postalRepository.GetById(783);
            Assert.IsNotNull(tempPostal);
            Assert.IsTrue(tempPostal.CityName == "Facility");
        }
    }
} 

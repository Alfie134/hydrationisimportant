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
        
        private IReadRepository<Postal> _postalRepository = new PostalRepository();


        [TestMethod]
        public void GetAll_ReturnsAllRows()
        {
           IEnumerable<Postal> PostalsList = _postalRepository.GetAll();
            Console.WriteLine("Postal number is this many: ");
            Console.WriteLine(PostalsList.Count());

        }
    }
}

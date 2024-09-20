using Configuration;
using Microsoft.Identity.Client;
using Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    internal class PostalRepository : IRepository<Postal>
    {
        private readonly string _connectionString = new AppConfig().ConnectionString;

        public void Add(Postal entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Postal> GetAll()
        {

        }

        public Postal GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Postal entity)
        {
            throw new NotImplementedException();
        }
    }
}

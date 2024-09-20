using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class MissionRepository: IRepository<Mission>
    {

        public IEnumerable<Mission> GetAll()
        {
            throw new NotImplementedException();
        }

        public Mission GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Mission entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

//        public void Add(Mission entity, SqlConnection connection, SqlTransaction? transaction = null)
//        {
//           throw new NotImplementedException();
//        }

        public void Add(Mission entity)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Models;

namespace Repositories.Interfaces
{
    public interface IMissionRepository: ICreateRepository<Mission>, IReadRepository<Mission>, IUpdateRepository<Mission>, IDeleteRepository<Mission>
    {
    }
}

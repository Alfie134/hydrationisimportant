using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces.Base;

namespace Repositories.Interfaces
{
    public interface IMissionRepository: ICreateRepository<Mission>, IReadRepository<Mission>, IUpdateRepository<Mission>, IDeleteRepository<Mission>
    {
        IEnumerable<Mission> GetMissionsByRouteId( int routeid, SqlConnection connection, SqlTransaction? transaction = null);
        IEnumerable<Mission> GetFilteredMissions(DateTime? selectedDate, bool showAllMissions , SqlConnection connection, SqlTransaction? transaction = null);
    }
}

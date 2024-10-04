using Models;
using Repositories.Interfaces.Base;

namespace Repositories.Interfaces
{
    public interface IRouteRepository : ICreateRepository<Route>, IReadRepository<Route>, IUpdateRepository<Route>
    {

    }
}

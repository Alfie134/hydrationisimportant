using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories.Interfaces.Base;

namespace Repositories.Interfaces
{
    public interface IVehicleRepository: IReadRepository<Vehicle>
    {
    }
}

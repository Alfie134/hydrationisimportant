using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Route
    {
        public int Id { get; set; }       // Unik identifikator for ruten
        public int VehicleId { get; set; }     // Identifikator for køretøjet, der bruges på ruten
        public List<Mission> Tasks { get; set; }  // Liste over opgaver knyttet til ruten

        // Konstruktor til at initialisere ruten
        public Route(int routeId, int vehicleId)
        {
            Id = routeId;
            VehicleId = vehicleId;
        }

        public Route( int vehicleId)
        {
            VehicleId = vehicleId;
        }
    }
}

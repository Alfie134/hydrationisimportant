using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Route
    {
        public int RouteId { get; set; }       // Unik identifikator for ruten
        public int VehicleId { get; set; }     // Identifikator for køretøjet, der bruges på ruten
        public List<Task> Tasks { get; set; }  // Liste over opgaver knyttet til ruten

        // Konstruktor til at initialisere ruten
        public Route(int routeId, int vehicleId)
        {
            RouteId = routeId;
            VehicleId = vehicleId;
            Tasks = new List<Task>(); // Initialiser en tom liste af opgaver
        }
    }
}

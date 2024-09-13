using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceOptimization.Model
{
    public class Region
    {
        public int RegionId { get; set; }  // Primær nøgle
        public string Name { get; set; }  // Regionens navn
        public string Itsystem { get; set; }  // IT-systemet, som regionen bruger, fx SimaTech eller Logis IDS 
    

        // Konstruktør
    public Region (int regionId, string name, string itsystem)
        {
            RegionId = regionId;
            Name = name;
            Itsystem = itsystem;

        }
    
    }

}

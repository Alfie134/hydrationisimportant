using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceOptimization.Model
{
    public class Vehicle
    {
        public int VehicleId { get; set; }  // Primær nøgle
        public string LicensePlate { get; set; }  // Nummerplade
        public string Location { get; set; }  // Placering (f.eks. GPS-koordinater eller adresse)
        public string Status { get; set; }  // Status (f.eks. Ledig, Optaget)
        public string Type { get; set; }  // Typen af køretøj (f.eks. Ambulance, Patienttransport)

        // Reference til region
        public int RegionId { get; set; }  // Fremmed nøgle til Region
        public Region Region { get; set; }
    }


}


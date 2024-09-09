using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceOptimization.Model
{
    public class Vehicle
    {
        public int Id { get; set; }  // Primær nøgle
        public VehicleType Type { get; set; }  // Typen af køretøj (f.eks. Ambulance, Patienttransport)

        // Konstruktor til at initialisere Vehicle-objektet
        public Vehicle(int id, VehicleType type)
        {
            Id = id;
            Type = type;
        }


    }


}


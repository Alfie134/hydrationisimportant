using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Vehicle
    {
        public int Id { get; set; }  // Primær nøgle
        public VehicleType Type { get; set; }  // Typen af køretøj (f.eks. Ambulance, Patienttransport)
        public Operator Operator { get; set; }
        public int? OperatorId { get; set; }

        // Parameterløs konstruktør
        public Vehicle() { }


        // Konstruktor til at initialisere Vehicle-objektet
        public Vehicle(int id, VehicleType type, int operatorId)
        {
            Id = id;
            Type = type;
            OperatorId = operatorId;
        }


    }


}


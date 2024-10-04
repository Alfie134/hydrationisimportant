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
        public string VehicleNumber { get; set; }
        public int OperatorId { get; set; }
        public int RegionId { get; set; }

        // Parameterløs konstruktør
        public Vehicle() { }


        // Konstruktor til at initialisere Vehicle-objektet
        public Vehicle(int id, string vehicleNumber, int operatorId,int regionsId)
        {
            Id = id;
            VehicleNumber = vehicleNumber;
            OperatorId = operatorId;
            RegionId = regionsId;
        }


    }


}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceOptimization.Model
{
    public class Task
    {
        public int Id { get; set; }  // Primær nøgle
        public int RegionId { get; set; }  // Fremmed nøgle til Region
        public string RegionalTaskId { get; set; }  // Regionalt specifikt opgave-ID

        public TaskType Type { get; set; }  // Typen af opgave (f.eks. Ambulance, Patienttransport)
        public string Description { get; set; }  // Opgavebeskrivelse

        public ServiceLevel ServiceLevel { get; set; }  
        public DateTime ExpectedDeparture { get; set; }
        public int DurationInMin { get; set; }
        public DateTime ExpectedArrival { get; set; }
        public string FromAddress { get; set; }
        public Postal FromPostal { get; set; }
        public string ToAddress { get; set; }
        public Postal ToPostal { get; set; }
        public string PatientName { get; set; }


        // Constructor
       public Task(int id, int regionId, string regionalTaskId, TaskType type, string description, ServiceLevel serviceLevel, DateTime expectedDeparture, 
                    int durationInMin, DateTime expectedArrival, string fromAddress, Postal fromPostal, string toAddress, Postal toPostak, string patientName)
  
        {
                Id = id; RegionId = regionId; RegionalTaskId = regionalTaskId; Type = type; Description = description; ServiceLevel = serviceLevel; 
            ExpectedDeparture = expectedDeparture; DurationInMin = durationInMin; ExpectedArrival = expectedArrival; FromAddress = fromAddress; 
            FromPostal = fromPostal; ToAddress = toAddress; ToPostal = toPostak; PatientName = patientName;
        }
    
    
    }

}

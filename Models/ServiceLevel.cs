using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ServiceLevel
    {
        public int Id { get; set; }    // Unik identifikator for service niveauet (måske respons).

        public string Name { get; set; }   // Navn på serviceniveauet (f.eks. "1 timer, "Standard responstid" eller noget ?).

        public TimeSpan Time { get; set; }   // Tidsgrænse for serviceniveauet (servicemålene).

        // Constructor 
        public ServiceLevel(int id, string name, TimeSpan time)
        {
            {
                Id = id;
                Name = name;
                Time = time;
            }

        }
    }
}



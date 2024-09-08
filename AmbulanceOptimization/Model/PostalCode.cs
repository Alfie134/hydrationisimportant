using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceOptimization.Model
{
    public class PostalCode
    {
        public int PostalCodeId { get; set; }  // postnummeret for byen 
        public string City { get; set; }  // Byen, der hører til postnummeret (f.eks. "Odense")
    }

}

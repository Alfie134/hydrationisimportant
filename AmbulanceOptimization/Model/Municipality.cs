using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceOptimization.Model
{
    public class Municipality
    {
        public int MunicipalityID { get; set; }  // Primær nøgle
        public string Name { get; set; }  // Kommunens navn (f.eks. "Odense Kommune")
    }

}

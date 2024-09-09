using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceOptimization.Model
{
    public class Municipality
    {
        public int Id { get; set; }  // Primær nøgle
        public string Name { get; set; }  // Kommunens navn (f.eks. "Odense Kommune")
   public int RegionId { get; set; }    // Id for regionen, som kommunen tilhører
        public List<int> Postals { get; set; }  // Liste over postnumre i kommunen

        // Constructor
        public Municipality(int id, string name, int regionId, List<int> postals) 
        {
            Id = id;
            Name = name;
            RegionId = regionId;
            Postals = postals ?? new List<int>(); //initiere listen over postnumre 
        }


    }

}

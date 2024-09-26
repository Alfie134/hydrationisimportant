using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models
{
    public class Municipality
    {
        public int Id { get; set; }  // Primær nøgle
        public string Name { get; set; }  // Kommunens navn (f.eks. "Odense Kommune")
        public int RegionId { get; set; }    // Id for regionen, som kommunen tilhører
        public List<int> PostalCodes { get; set; }  // Liste over postnumre i kommunen

        // Constructor
        public Municipality(int id, string name, int regionId)
        {
            Id = id;
            Name = name;
            RegionId = regionId;
        }


    }

}

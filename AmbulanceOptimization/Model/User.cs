using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceOptimization.Model
{
    public class User
    {
        public int UserId { get; set; }  // Primær nøgle
        public string Username { get; set; }  // Brugernavn
        public string Password { get; set; }  // Adgangskode (husk kryptering/hashing af password!)
        public string Role { get; set; }  // Rolle (f.eks. "Dispatcher", "Admin")

        // Optionelt: Reference til en region (hvis brugere er knyttet til en region)
        public int? RegionId { get; set; }  // Fremmed nøgle til Region (nullable hvis det ikke er alle brugere, der har en region)
        public Region Region { get; set; }
    }

}

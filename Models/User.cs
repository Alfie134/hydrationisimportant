using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }  // Brugernavn
        public string Password { get; set; }  // Adgangskode (husk kryptering/hashing af password!)
        public Region Region { get; set; }  // Offentligt reference til den region brugeren tilhører
        // Optionelt: Reference til en region (hvis brugere ikke er knyttet til en region)
        public int? RegionId { get; set; }  // Fremmed nøgle til Region (nullable hvis det ikke er alle brugere, der har en region)

        // Constructor til at initialisere User-objektet med værdier.
        public User(int id, string userName, string password, int? regionId)
        {
            Id = id;
            UserName = userName;
            Password = password; // Password bør håndteres sikkert (f.eks. hashing).
            RegionId = regionId;
        }
    }

}

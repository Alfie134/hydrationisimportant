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
        public string UserName { get; set; }  // Brugernavn
        public string Password { get; set; }  // Adgangskode (husk kryptering/hashing af password!)
        public Region Region { get; set; }  // Offentligt reference til den region brugeren tilhører.


        // Constructor til at initialisere User-objektet med værdier.
        public User(string userName, string password, Region region)
        {

            UserName = userName;
            Password = password; // Password bør håndteres sikkert (f.eks. hashing).
            Region = region;
        }



        // Optionelt: Reference til en region (hvis brugere ikke er knyttet til en region)
        public int? RegionId { get; set; }  // Fremmed nøgle til Region (nullable hvis det ikke er alle brugere, der har en region)


    }

}

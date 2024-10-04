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
        public string PasswordHash { get; set; }  // Adgangskode (husk kryptering/hashing af password!)
        public string PasswordSalt { get; set; }
        public Region Region { get; set; }  // Offentligt reference til den region brugeren tilhører.
        public int? RegionId { get; set; }  // Fremmed nøgle til Region (nullable hvis det ikke er alle brugere, der har en region)

        // Constructor til at initialisere User-objektet med værdier.
        public User(string userName, string passwordHash, int region, string passwordSalt)
        {

            UserName = userName;
            PasswordHash = passwordHash; // PasswordHash bør håndteres sikkert (f.eks. hashing).
            RegionId = region;
            PasswordSalt = passwordSalt;
        }

        public User(int id,string userName, string passwordHash, int region, string passwordSalt)
        {
            Id = id;
            UserName = userName;
            PasswordHash = passwordHash; // PasswordHash bør håndteres sikkert (f.eks. hashing).
            RegionId = region;
            PasswordSalt = passwordSalt;
        }


        // Optionelt: Reference til en region (hvis brugere ikke er knyttet til en region)



    }

}

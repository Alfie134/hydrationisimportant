namespace Models
{
    public class Postal
    {
        public int PostalNumber { get; set; }  // postnummeret for byen 
        public string CityName { get; set; }  // Byen, der hører til postnummeret (f.eks. "Odense")

        // Constructor
        public Postal(int postalNumber, string cityName)
        {
            PostalNumber = postalNumber;
            CityName = cityName;
        }
    }



}

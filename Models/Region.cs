﻿namespace Models
{
    public class Region
    {
        public int RegionId { get; set; }  // Primær nøgle
        public string Name { get; set; }  // Regionens navn
        public string? ItSystem { get; set; }  // IT-systemet, som regionen bruger, fx SimaTech eller Logis IDS 

        // Konstruktør
        public Region(int regionId, string name, string? itsystem)
        {
            RegionId = regionId;
            Name = name;
            ItSystem = itsystem;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceOptimization.Model
{
    public class Task
    {
        public int TaskId { get; set; }  // Primær nøgle
        public int RegionId { get; set; }  // Fremmed nøgle til Region
        public string RegionalTaskId { get; set; }  // Regionalt specifikt opgave-ID

        public string TaskType { get; set; }  // Typen af opgave (f.eks. Ambulance, Patienttransport)
        public string Description { get; set; }  // Opgavebeskrivelse

        public TimeSpan ServiceGoal { get; set; }  // Servicemål, f.eks. maksimalt tidsinterval
        public DateTime TaskDateTime { get; set; }  // Dato og tid for opgaven

        public string FromAddress { get; set; }  // Fra adresse (afhentningsadresse)
        public int FromPostalCodeId { get; set; }  // Fremmed nøgle til postnummer for fra-adresse
        public PostalCode FromPostalCode { get; set; }  // Navigation property

        public string ToAddress { get; set; }  // Til adresse (leveringsadresse)
        public int ToPostalCodeId { get; set; }  // Fremmed nøgle til postnummer for til-adresse
        public PostalCode ToPostalCode { get; set; }  // Navigation property

        public string PatientName { get; set; }  // Patientens navn
    }

}

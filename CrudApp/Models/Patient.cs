using System.Text.Json.Serialization;

namespace CrudApp.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string? PatietName { get; set; }
        public string ?patientDisease { get; set; }
        public int Age { get; set; }
        public ICollection<Address>? AddressField { get; set; } = new List<Address>();

    }

    public class Address
       { 
        public int Id { get; set; }
         public string ? District { get; set; }
        public string? Street { get; set; }
        public string? Tole { get; set; }
        public int  PatientId { get; set; }
        [JsonIgnore]
        public Patient? Patients { get; set; }
    }

}

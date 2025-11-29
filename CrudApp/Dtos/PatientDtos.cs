namespace CrudApp.Dtos
{
    public class AddPatientDtos
    {
        public string Name { get; set; }
        public string Disease { get; set; }
        public int age { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }

    public class AddressDto
    {
        public string District { get; set; }
        public string Street { get; set; }
        public string Tole { get; set; }
    }
}

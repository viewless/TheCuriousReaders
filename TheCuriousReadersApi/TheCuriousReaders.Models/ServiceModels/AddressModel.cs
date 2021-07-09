namespace TheCuriousReaders.Models.ServiceModels
{
    public class AddressModel
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string BuildingNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string AdditionalInfo { get; set; }
    }
}

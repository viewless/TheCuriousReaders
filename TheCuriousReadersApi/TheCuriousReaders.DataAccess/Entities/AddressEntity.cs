using System.Collections.Generic;

namespace TheCuriousReaders.DataAccess.Entities
{
    public class AddressEntity
    {
        public AddressEntity()
        {
            Users = new List<UserEntity>();
        }

        public int Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public string BuildingNumber { get; set; }

        public string ApartmentNumber { get; set; }

        public string AdditionalInfo { get; set; }

        public ICollection<UserEntity> Users { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TheCuriousReaders.DataAccess.Entities;
using TheCuriousReaders.DataAccess.Interfaces;

namespace TheCuriousReaders.DataAccess.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly CuriousReadersContext _curiousReadersContext;

        public AddressRepository(CuriousReadersContext curiousReadersContext)
        {
            _curiousReadersContext = curiousReadersContext;
        }

        public async Task<AddressEntity> GetAddressAsync(AddressEntity addressEntity)
        {
            return await _curiousReadersContext.Addresses
                .Where(address => address.Country.ToLower() == addressEntity.Country.ToLower()
                && address.City.ToLower() == addressEntity.City.ToLower()
                && address.BuildingNumber.ToLower() == addressEntity.BuildingNumber.ToLower()
                && address.Street.ToLower() == addressEntity.Street.ToLower()
                && address.StreetNumber.ToLower() == addressEntity.StreetNumber.ToLower()
                && address.ApartmentNumber.ToLower() == addressEntity.ApartmentNumber.ToLower())
                .FirstOrDefaultAsync();
        }
    }
}

using System.Threading.Tasks;
using TheCuriousReaders.DataAccess.Entities;

namespace TheCuriousReaders.DataAccess.Interfaces
{
    public interface IAddressRepository
    {
        Task<AddressEntity> GetAddressAsync(AddressEntity addressEntity);
    }
}

using System.Threading.Tasks;
using TheCuriousReaders.Models.ResponseModels;
using TheCuriousReaders.Models.ServiceModels;

namespace TheCuriousReaders.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(UserModel model);

        Task<LoginResponse> LoginAsync(UserLoginModel model);
        Task<int> GetTotalAccountsCountAsync();
    }
}

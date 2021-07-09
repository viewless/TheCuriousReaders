using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TheCuriousReaders.Models.ServiceModels;

namespace TheCuriousReaders.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUserAsync(UserModel model);

        Task<bool> ExistsAsync(string email, string password);
        Task<int> GetTotalAccountsAsync();
    }
}

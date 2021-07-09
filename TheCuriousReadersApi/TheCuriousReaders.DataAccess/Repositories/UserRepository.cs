using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TheCuriousReaders.DataAccess.Entities;
using TheCuriousReaders.DataAccess.Interfaces;
using TheCuriousReaders.Models.ServiceModels;

namespace TheCuriousReaders.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;
        private readonly CuriousReadersContext _curiousReadersContext;

        public UserRepository(
            UserManager<UserEntity> userManager,
            IMapper mapper, 
            IAddressRepository addressRepository,
            CuriousReadersContext curiousReadersContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _addressRepository = addressRepository;
            _curiousReadersContext = curiousReadersContext;
        }

        public async Task<IdentityResult> CreateUserAsync(UserModel model)
        {
            var entity = _mapper.Map<UserEntity>(model);

            //Call find method for Address in case a similar address exists so we don't have to create duplicates
            var address = await _addressRepository.GetAddressAsync(entity.Address);

            if (!(address is null))
            {
                entity.Address = address;
            }

            var result = await _userManager.CreateAsync(entity, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(entity, "Customer");
            }

            return result;
        }

        public async Task<bool> ExistsAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return false;
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, password);

            return passwordValid;
        }

        public async Task<int> GetTotalAccountsAsync()
        {
            return await _curiousReadersContext.Users.CountAsync();
        }
    }
}

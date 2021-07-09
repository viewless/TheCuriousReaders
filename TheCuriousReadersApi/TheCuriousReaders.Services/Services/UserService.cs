using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCuriousReaders.DataAccess.Entities;
using TheCuriousReaders.DataAccess.Interfaces;
using TheCuriousReaders.Models.Enums;
using TheCuriousReaders.Models.Helpers;
using TheCuriousReaders.Models.ResponseModels;
using TheCuriousReaders.Models.ServiceModels;
using TheCuriousReaders.Services.CustomExceptions;
using TheCuriousReaders.Services.Interfaces;

namespace TheCuriousReaders.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly UserManager<UserEntity> _userManager;

        public UserService(
            IUserRepository userRepository,
            IJwtTokenGenerator jwtTokenGenerator,
            UserManager<UserEntity> userManager)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
        }

        public async Task<bool> CreateUserAsync(UserModel model)
        {
            //Validate the country from possible list of countries
            if (!(CountriesValidator.IsCountryValid(model.Address.Country)))
            {
                throw new ValidationException($"The country '{model.Address.Country}' does not exist in our database.");
            }

            /* Check if the password is valid - it should be at least 10 symbols, 
            * it should have an uppercase and lowercase letter,
            a symbol and a number. The validation rules are written in the Startup.cs class. */
            IdentityResult result = new IdentityResult();

            if (await ValidatePasswordAsync(model.Password))
            {
                model.UserName = model.EmailAddress;

                result = await _userRepository.CreateUserAsync(model);

                //If it passes the Identity checks for duplicate records and it created the record, we return true
                if (result.Succeeded)
                {
                    return true;
                }
            }

            //We check the errors here and in case they are because of an already existing email, we inform the user
            CheckForDuplicateEmailError(result);

            //In case any other Identity error occurs during creation, return false
            return false;
        }
        public async Task<LoginResponse> LoginAsync(UserLoginModel model)
        {
            if (!await _userRepository.ExistsAsync(model.Email, model.Password))
            {
                return new LoginResponse { Token = null };
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            return new LoginResponse { Token = token };
        }

        private async Task<bool> ValidatePasswordAsync(string password)
        {
            var passwordErrors = new List<string>();

            var validators = _userManager.PasswordValidators;

            foreach (var validator in validators)
            {
                var result = await validator.ValidateAsync(_userManager, null, password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        passwordErrors.Add(error.Description);
                    }
                }
            }

            /* If the passwordErrors variable contains any errors,
             * we must inform the consumer of the API that the password's validation
             * wasn't accepted and return the errors. */
            if (passwordErrors.Any())
            {
                StringBuilder stringBuilder = new StringBuilder();

                foreach (var passwordError in passwordErrors)
                {
                    stringBuilder.Append(passwordError);
                }

                throw new ValidationException(stringBuilder.ToString());
            }

            return true;
        }

        private void CheckForDuplicateEmailError(IdentityResult identityResult)
        {
            foreach (var errors in identityResult.Errors)
            {
                if (errors.Code.Equals(ErrorEnum.DuplicateEmail.ToString()))
                {
                    throw new DuplicateResourceException("Email already exists.");
                }
            }
        }

        public async Task<int> GetTotalAccountsCountAsync()
        {
            return await _userRepository.GetTotalAccountsAsync();
        }
    }
}

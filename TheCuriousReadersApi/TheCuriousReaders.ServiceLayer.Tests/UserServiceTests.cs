using Microsoft.AspNetCore.Identity;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCuriousReaders.DataAccess.Entities;
using TheCuriousReaders.Models.ResponseModels;
using TheCuriousReaders.Models.ServiceModels;
using TheCuriousReaders.ServiceLayer.Tests.Fixtures;
using TheCuriousReaders.Services.CustomExceptions;
using Xunit;

namespace TheCuriousReaders.ServiceLayer.Tests
{
    public class UserServiceTests : IClassFixture<UserServiceFixture>
    {
        private readonly UserServiceFixture _userServiceFixture;

        public UserServiceTests(UserServiceFixture userServiceFixture)
        {
            _userServiceFixture = userServiceFixture;
        }

        [Fact]
        public async Task When_CountryDoesNotExistInOurList_Expect_ValidationException()
        {
            //Arrange
            var addressModel = new AddressModel
            {
                Country = "Poland",
                City = "Krakow",
                Street = "Jan Sobieski",
                StreetNumber = "33"
            };

            var userModel = new UserModel
            {
                FirstName = "Ivan",
                LastName = "Petrov",
                Address = addressModel,
                Password = "lakdfjjj6A#",
                EmailAddress = "mariya.petkanova@abv.bg",
                ConfirmPassword = "lakdfjjj6A#",
                UserName = "mariya.petkanova@abv.bg",
                PhoneNumber = "0873432432"
            };

            //Act
            var exception = await Record.ExceptionAsync(async () => await _userServiceFixture.UserService.CreateUserAsync(userModel));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
        }

        [Fact]
        public async Task When_ProvidedPasswordIsInCorrectFormat_Expect_True()
        {
            //Arrange
            var addressModel = new AddressModel
            {
                Country = "Bulgaria",
                City = "Plovdiv",
                Street = "Ivan Vazov",
                StreetNumber = "12"
            };

            var userModel = new UserModel
            {
                FirstName = "Ivan",
                LastName = "Petrov",
                Address = addressModel,
                Password = "lakdfjjj6A#",
                EmailAddress = "mariya.petkanova@abv.bg",
                ConfirmPassword = "lakdfjjj6A#",
                UserName = "mariya.petkanova@abv.bg",
                PhoneNumber = "0873432432"
            };

            _userServiceFixture.UserRepositoryMock
                .Setup(userRepository => userRepository.CreateUserAsync(userModel)).ReturnsAsync(IdentityResult.Success);

            //Act
            var result = await _userServiceFixture.UserService.CreateUserAsync(userModel);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task When_ProvidedPasswordIsInIncorrectFormat_Expect_ValidationException()
        {
            //Arrange
            var addressModel = new AddressModel
            {
                Country = "Bulgaria",
                City = "Plovdiv",
                Street = "Ivan Vazov",
                StreetNumber = "12"
            };

            var userModel = new UserModel
            {
                FirstName = "Ivan",
                LastName = "Petrov",
                Address = addressModel,
                Password = "lakdff",
                EmailAddress = "mariya.petkanova@abv.bg",
                ConfirmPassword = "lakdff",
                UserName = "mariya.petkanova@abv.bg",
                PhoneNumber = "0873432432"
            };

            _userServiceFixture.UserRepositoryMock
                .Setup(userRepository => userRepository.CreateUserAsync(userModel)).ReturnsAsync(IdentityResult.Success);

            //Act
            var exception = await Record.ExceptionAsync(async () => await _userServiceFixture.UserService.CreateUserAsync(userModel));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
        }

        [Fact]
        public async Task When_ProvidedEmailAlreadyExists_Expect_DuplicateResourceException()
        {
            //Arrange
            var addressModel = new AddressModel
            {
                Country = "Bulgaria",
                City = "Plovdiv",
                Street = "Ivan Vazov",
                StreetNumber = "22"
            };

            var userModel = new UserModel
            {
                FirstName = "Ivan",
                LastName = "Petrov",
                Address = addressModel,
                Password = "lakdfjjj6A#",
                EmailAddress = "mariya.petkanova@abv.bg"
            };

            var identityError = new IdentityError();
            identityError.Code = "DuplicateEmail";

            _userServiceFixture.UserRepositoryMock
                .Setup(userRepository => userRepository.CreateUserAsync(userModel)).ReturnsAsync(IdentityResult.Failed(identityError));

            //Act
            var exception = await Record.ExceptionAsync(async () => await _userServiceFixture.UserService.CreateUserAsync(userModel));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<DuplicateResourceException>(exception);
        }

        [Fact]
        public async Task When_AnIdentityErrorDuringRegistrationOccurs_Expect_False()
        {
            //Arrange
            var addressModel = new AddressModel
            {
                Country = "Bulgaria",
                City = "Plovdiv",
                Street = "Ivan Vazov",
                StreetNumber = "33"
            };

            var userModel = new UserModel
            {
                FirstName = "Ivan",
                LastName = "Petrov",
                Address = addressModel,
                Password = "lakdfjjj6A#",
                EmailAddress = "mariya.petkanova@abv.bg"
            };

            var identityError = new IdentityError();
            identityError.Code = "WrongUserName";

            _userServiceFixture.UserRepositoryMock
                .Setup(userRepository => userRepository.CreateUserAsync(userModel)).ReturnsAsync(IdentityResult.Failed(identityError));

            //Act
            var result = await _userServiceFixture.UserService.CreateUserAsync(userModel);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task When_UserWithProvidedEmailDoesNotExist_Expect_LoginResponseWithNullToken()
        {
            //Arrange
            var userModel = new UserLoginModel
            {
                Email = "mihail@abv.bg",
                Password = "83u49DHAS984h329##DAKDVN()"
            };

            _userServiceFixture.UserRepositoryMock
                .Setup(userRepository => userRepository.ExistsAsync(userModel.Email, userModel.Password)).ReturnsAsync(false);

            //Act
            var result = await _userServiceFixture.UserService.LoginAsync(userModel);

            //Assert
            Assert.Null(result.Token);
            Assert.IsType<LoginResponse>(result);
        }

        [Fact]
        public async Task When_UserWithProvidedEmailAlreadyExists_Expect_LoginResponseWithNullTokaden()
        {
            //Arrange
            var userLoginModel = new UserLoginModel
            {
                Email = "mihail@abv.bg",
                Password = "83u49DHAS984h329##DAKDVN()"
            };

            var userEntity = new UserEntity
            {
                Email = "mihail@abv.bg"
            };

            var roles = new List<string>();
            roles.Add("Librarian");

            string jwtTokenExample = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9" +
                ".eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ" +
                ".SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";

            _userServiceFixture.UserRepositoryMock
                .Setup(userRepository => userRepository.ExistsAsync(userLoginModel.Email, userLoginModel.Password)).ReturnsAsync(true);
            _userServiceFixture.UserManagerMock
                .Setup(userManager => userManager.FindByEmailAsync(userLoginModel.Email)).ReturnsAsync(userEntity);
            _userServiceFixture.UserManagerMock
                .Setup(userManager => userManager.GetRolesAsync(userEntity)).ReturnsAsync(roles);
            _userServiceFixture.JwtGeneratorMock
                .Setup(jwtGenerator => jwtGenerator.GenerateToken(userEntity, roles)).Returns(jwtTokenExample);

            //Act
            var result = await _userServiceFixture.UserService.LoginAsync(userLoginModel);

            //Assert
            Assert.NotNull(result.Token);
            Assert.IsType<LoginResponse>(result);
        }
    }
}

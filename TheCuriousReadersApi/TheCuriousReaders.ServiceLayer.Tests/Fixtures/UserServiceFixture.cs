using Microsoft.AspNetCore.Identity;
using Moq;
using System.Collections.Generic;
using TheCuriousReaders.DataAccess.Entities;
using TheCuriousReaders.DataAccess.Interfaces;
using TheCuriousReaders.Services.Interfaces;
using TheCuriousReaders.Services.Services;

namespace TheCuriousReaders.ServiceLayer.Tests.Fixtures
{
    public class UserServiceFixture
    {
        public Mock<IUserRepository> UserRepositoryMock { get; private set; }
        public IUserService UserService { get; private set; }
        public Mock<IJwtTokenGenerator> JwtGeneratorMock { get; private set; }
        public Mock<UserManager<UserEntity>> UserManagerMock { get; private set; }

        public UserServiceFixture()
        {
            this.UserRepositoryMock = new Mock<IUserRepository>();
            this.JwtGeneratorMock = new Mock<IJwtTokenGenerator>();
            var PasswordValidators = new List<IPasswordValidator<UserEntity>>();
            PasswordValidators.Add(new PasswordValidator<UserEntity>());
            var userStoreMock = new Mock<IUserStore<UserEntity>>();
            this.UserManagerMock = new Mock<UserManager<UserEntity>>(userStoreMock.Object, null, null, null, PasswordValidators, null, null, null, null);

            this.UserService = new UserService(UserRepositoryMock.Object, JwtGeneratorMock.Object, UserManagerMock.Object);
        }
    }
}

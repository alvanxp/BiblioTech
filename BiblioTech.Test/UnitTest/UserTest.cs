using BiblioTech.Data;
using BiblioTech.Domain.Dto;
using BiblioTech.Domain.Entities;
using BiblioTech.Services;
using BiblioTech.Services.Authentication;
using BiblioTech.Services.UserService;
using Microsoft.Extensions.Options;
using Moq;

namespace BiblioTech.Test.UnitTest
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _userService;
        private readonly Mock<AuthenticationService> _authenticationServiceMock;
        public UserServiceTests()
        {
            var appSettings = Options.Create(new JwtSettings { Secret = "test-secret" });
            _userRepositoryMock = new Mock<IUserRepository>();
            _authenticationServiceMock = new Mock<AuthenticationService>(appSettings);
            _userService = new UserService(_userRepositoryMock.Object, _authenticationServiceMock.Object); 
        }

        [Fact]
        public async Task Authenticate_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var model = new AuthenticateRequest
            {
                Username = "testuser",
                Password = "testpassword"
            };
            _userRepositoryMock.Setup(x => x.GetUserByUsername(model.Username)).ReturnsAsync((User)null);

            // Act
            var result = await _userService.Authenticate(model);

            // Assert
            Assert.Null(result.Data?.Token);
        }
        

        [Fact]
        public async Task Register_InvalidRequest_ReturnsNull()
        {
            // Arrange
            var registerRequest = new RegisterRequest
            {
                FirstName = "John",
                LastName = "Doe",
                Username = "johndoe",
                Password = "testpassword"
            };
            _userRepositoryMock.Setup(x => x.Insert(It.IsAny<User>())).ReturnsAsync((User)null);

            // Act
            var result = await _userService.Register(registerRequest);

            // Assert
            Assert.Null(result.Data?.Token);
        }
    }
}

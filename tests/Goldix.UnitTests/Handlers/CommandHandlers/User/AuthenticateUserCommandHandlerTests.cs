using Goldix.Application.Commands.User;
using Goldix.Application.Exceptions;
using Goldix.Application.Interfaces.Services.Identity;
using Goldix.Application.Models.User.GetToken;
using Goldix.Domain.Entities.User;
using Goldix.Infrastructure.Handlers.CommandHandlers.User;
using System.Security.Claims;

namespace Goldix.UnitTests.Handlers.CommandHandlers.User;

public class AuthenticateUserCommandHandlerTests
{
    private readonly Mock<IAuthenticationService> _authenticationServiceMock;
    private readonly Mock<IClaimsService> _claimServiceMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly AuthenticateUserCommandHandler _handler;

    public AuthenticateUserCommandHandlerTests()
    {
        _authenticationServiceMock = new Mock<IAuthenticationService>();
        _claimServiceMock = new Mock<IClaimsService>();
        _tokenServiceMock = new Mock<ITokenService>();
        _handler = new AuthenticateUserCommandHandler(_authenticationServiceMock.Object, _claimServiceMock.Object, _tokenServiceMock.Object);
    }

    [Fact]
    public async Task Handle_WhenUserIsNotValid_ShouldThrowBadRequestException()
    {
        // Arrange
        var query = new AuthenticateUserCommand(new GetTokenRequestDto());

        _authenticationServiceMock.Setup(x => x.ValidateUserAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Throws<BadRequestException>();

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenValidCommand_ShouldReturnGetTokenResponseDto()
    {
        // Arrange
        var query = new AuthenticateUserCommand(new GetTokenRequestDto { UserName = "testuser", Password = "password" });

        var user = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = "testuser", CreatedAt = DateTime.Now, FirstName = "testuser", LastName = "testuser" };
        var claims = new List<Claim> { new Claim("claimType", "claimValue") };
        var tokenResponse = new GetTokenResponseDto { Token = "token", ExpirationAt = DateTime.UtcNow.AddHours(1), UserId = user.Id };

        _authenticationServiceMock.Setup(x => x.ValidateUserAsync(query.dto.UserName, query.dto.Password, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        _claimServiceMock.Setup(x => x.GenerateUserClaimsAsync(user))
            .ReturnsAsync(claims);
        _tokenServiceMock.Setup(x => x.GenerateToken(user, claims))
            .Returns(tokenResponse);
        _tokenServiceMock.Setup(x => x.EncryptToken(tokenResponse.Token))
            .Returns("encryptedToken");

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("encryptedToken", result.Token);
        Assert.Equal(tokenResponse.ExpirationAt, result.ExpirationAt);
        Assert.Equal(user.Id, result.UserId);
    }
}

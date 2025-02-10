using System;
using System.Threading.Tasks;
using Bogus.DataSets;
using Default.Domain.Entities;
using Default.Domain.Interfaces.Core.Events;
using Default.Domain.Interfaces.DomainServices;
using Default.Domain.Interfaces.Infrastructure.Repositories;
using Default.Domain.Structs;
using Default.DomainServices.Services;
using EscNet.Hashers.Interfaces.Algorithms;
using FluentAssertions;
using Moq;
using Xunit;

namespace Otanimes.Tests.DomainServices;

public class UserDomainServiceTests
{
    private readonly IUserDomainService _sut;

    private readonly Mock<IServiceProvider> providerMock = new();
    private readonly Mock<IArgon2IdHasher> hasherMock = new();
    private readonly Mock<IDomainNotificationFacade> domainNotificationMock = new();
    private readonly Mock<IUserRepository> userRepositoryMock = new();
 
    public UserDomainServiceTests()
    {
        _sut = new UserDomainService(
            provider: providerMock.Object,
            hasher: hasherMock.Object,
            domainNotification: domainNotificationMock.Object, 
            userRepository: userRepositoryMock.Object);
    }

    [Fact(DisplayName = "LoginAsync when user not exists by username or password throw password mismatch notification and returns empty optional")]
    [Trait("UserDomainService", "LoginAsync")]
    public async Task LoginAsync_WhenUserNotExistsByUsernameOrPassword_ThrowPasswordMismatchNotificationAndReturnsEmptyOptional()
    {
        // Arrange
        var username = new Internet().UserName();
        var password = new Internet().Password();
        var hashedPassword = new Internet().Password();

        hasherMock.Setup(s => s.Hash(password))
            .Returns(hashedPassword);

        userRepositoryMock.Setup(s => s.ExistsAsync(e =>
                e.Username.ToLower() == username.ToLower() &&
                e.Password == hashedPassword))
            .ReturnsAsync(false);
        
        domainNotificationMock.Setup(setup => setup.PublishInvalidCredentialsAsync())
            .Verifiable();

        // Act
        var result = await _sut.LoginAsync(username, password);

        // Assert
        hasherMock.Verify(s => s.Hash(password),
            Times.Once);

        userRepositoryMock.Verify(s => s.ExistsAsync(e =>
                e.Username.ToLower() == username.ToLower() &&
                e.Password == hashedPassword),
            Times.Once);
        
        domainNotificationMock.Verify(setup => setup.PublishInvalidCredentialsAsync(),
            Times.Once);
        
        result.Should()
            .BeEquivalentTo(new Optional<User>());
    }
}
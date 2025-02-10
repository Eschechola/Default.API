using System;
using System.Threading.Tasks;
using EscNet.Hashers.Interfaces.Algorithms;
using Default.Domain.Entities;
using Default.Domain.Interfaces.Core.Events;
using Default.Domain.Interfaces.DomainServices;
using Default.Domain.Interfaces.Infrastructure.Repositories;
using Default.Domain.Structs;

namespace Default.DomainServices.Services;

public class UserDomainService(
    IServiceProvider provider,
    IArgon2IdHasher hasher,
    IDomainNotificationFacade domainNotification,
    IUserRepository userRepository) : DomainService(provider), IUserDomainService
{
    public async Task<Optional<User>> LoginAsync(string username, string password)
    {
        if (await UserNotExistsAsync(username, password))
            return new Optional<User>();

        var user = await userRepository.GetByUsernameAsync(username);
        await UpdateLastLoginAsync(user);

        return user;
    }

    private async Task UpdateLastLoginAsync(User user)
    {
        user.LastLoginAtNow();
        userRepository.Update(user);
        await userRepository.UnitOfWork.SaveChangesAsync();
    }

    private async Task<bool> UserNotExistsAsync(string username, string password)
    {
        var hashedPassword = hasher.Hash(password);
        
        if (await userRepository.ExistsAsync(entity =>
                entity.Username.ToLower() == username.ToLower() &&
                entity.Password == hashedPassword))
            return false;

        await domainNotification.PublishInvalidCredentialsAsync();
        return true;
    }
}
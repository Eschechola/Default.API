using System;
using System.Threading.Tasks;
using EscNet.Hashers.Interfaces.Algorithms;
using Otanimes.Domain.Entities;
using Otanimes.Domain.Interfaces.DomainServices;
using Otanimes.Domain.Interfaces.Infrastructure.Repositories;
using Otanimes.Domain.Structs;

namespace Otanimes.DomainServices.Services;

public class UserDomainService(
    IServiceProvider provider,
    IArgon2IdHasher hasher,
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
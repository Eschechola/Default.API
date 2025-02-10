using System.Threading.Tasks;
using Default.ApplicationServices.Mappers;
using Default.Domain.DTOs.Entities;
using Default.Domain.Interfaces.ApplicationServices;
using Default.Domain.Interfaces.DomainServices;
using Default.Domain.Structs;

namespace Default.ApplicationServices.Services;

public class UserApplicationService(IUserDomainService userDomainService) : IUserApplicationService 
{
    public async Task<Optional<UserDto>> LoginAsync(string username, string password)
    {
        var user = await userDomainService.LoginAsync(username, password);

        return user.IsEmpty
            ? new Optional<UserDto>()
            : user.Value.AsDto();
    }
}
using System.Threading.Tasks;
using Otanimes.ApplicationServices.Mappers;
using Otanimes.Domain.DTOs.Entities;
using Otanimes.Domain.Interfaces.ApplicationServices;
using Otanimes.Domain.Interfaces.DomainServices;
using Otanimes.Domain.Structs;

namespace Otanimes.ApplicationServices.Services;

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
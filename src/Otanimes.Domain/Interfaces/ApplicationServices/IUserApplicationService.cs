using System.Threading.Tasks;
using Otanimes.Domain.DTOs.Entities;
using Otanimes.Domain.Structs;

namespace Otanimes.Domain.Interfaces.ApplicationServices;

public interface IUserApplicationService : IApplicationService
{
    Task<Optional<UserDto>> LoginAsync(string username, string password);
}
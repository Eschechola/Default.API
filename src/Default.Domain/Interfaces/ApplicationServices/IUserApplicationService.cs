using System.Threading.Tasks;
using Default.Domain.DTOs.Entities;
using Default.Domain.Structs;

namespace Default.Domain.Interfaces.ApplicationServices;

public interface IUserApplicationService : IApplicationService
{
    Task<Optional<UserDto>> LoginAsync(string username, string password);
}
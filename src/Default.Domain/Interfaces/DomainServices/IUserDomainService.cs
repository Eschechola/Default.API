using System.Threading.Tasks;
using Default.Domain.Entities;
using Default.Domain.Structs;

namespace Default.Domain.Interfaces.DomainServices;

public interface IUserDomainService : IDomainService
{
    Task<Optional<User>> LoginAsync(string username, string password);
}
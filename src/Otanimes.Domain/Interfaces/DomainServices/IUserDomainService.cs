using System.Threading.Tasks;
using Otanimes.Domain.Entities;
using Otanimes.Domain.Structs;

namespace Otanimes.Domain.Interfaces.DomainServices;

public interface IUserDomainService : IDomainService
{
    Task<Optional<User>> LoginAsync(string username, string password);
}
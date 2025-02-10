using System.Threading.Tasks;
using Otanimes.Domain.Entities;

namespace Otanimes.Domain.Interfaces.Infrastructure.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByUsernameAsync(string username);
}
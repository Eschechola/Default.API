using System.Threading.Tasks;
using Default.Domain.Entities;

namespace Default.Domain.Interfaces.Infrastructure.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByUsernameAsync(string username);
}
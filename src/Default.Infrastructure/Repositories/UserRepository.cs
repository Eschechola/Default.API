using System.Threading.Tasks;
using Default.Domain.Entities;
using Default.Domain.Interfaces.Infrastructure;
using Default.Domain.Interfaces.Infrastructure.Repositories;
using Default.Infrastructure.Context;

namespace Default.Infrastructure.Repositories;

public class UserRepository(DefaultContext context) : Repository<User>(context), IUserRepository
{
    private readonly DefaultContext _context = context;
    public override IUnitOfWork UnitOfWork => _context;

    public async Task<User> GetByUsernameAsync(string username)
        => await GetAsync(entity => entity.Username.ToLower() == username.ToLower());
}
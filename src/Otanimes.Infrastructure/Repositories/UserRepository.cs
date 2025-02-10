using System.Threading.Tasks;
using Otanimes.Domain.Entities;
using Otanimes.Domain.Interfaces.Infrastructure;
using Otanimes.Domain.Interfaces.Infrastructure.Repositories;
using Otanimes.Infrastructure.Context;

namespace Otanimes.Infrastructure.Repositories;

public class UserRepository(OtanimesContext context) : Repository<User>(context), IUserRepository
{
    private readonly OtanimesContext _context = context;
    public override IUnitOfWork UnitOfWork => _context;

    public async Task<User> GetByUsernameAsync(string username)
        => await GetAsync(entity => entity.Username.ToLower() == username.ToLower());
}
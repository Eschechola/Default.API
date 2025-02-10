using Otanimes.Domain.Entities;
using Otanimes.Domain.Interfaces.Infrastructure;
using Otanimes.Domain.Interfaces.Infrastructure.Repositories;
using Otanimes.Infrastructure.Context;

namespace Otanimes.Infrastructure.Repositories;

public class AddressRepository(OtanimesContext context) : Repository<Address>(context), IAddressRepository
{
    private readonly OtanimesContext _context = context;
    
    public override IUnitOfWork UnitOfWork => _context;
}
using Default.Domain.Entities;
using Default.Domain.Interfaces.Infrastructure;
using Default.Domain.Interfaces.Infrastructure.Repositories;
using Default.Infrastructure.Context;

namespace Default.Infrastructure.Repositories;

public class AddressRepository(OtanimesContext context) : Repository<Address>(context), IAddressRepository
{
    private readonly OtanimesContext _context = context;
    
    public override IUnitOfWork UnitOfWork => _context;
}
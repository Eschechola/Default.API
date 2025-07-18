using Default.Domain.Entities;
using Default.Domain.Interfaces.Infrastructure;
using Default.Domain.Interfaces.Infrastructure.Repositories;
using Default.Infrastructure.Context;

namespace Default.Infrastructure.Repositories;

public class ContactRepository(DefaultContext context) : Repository<Contact>(context), IContactRepository
{
    private readonly DefaultContext _context = context;
    public override IUnitOfWork UnitOfWork => _context;
}
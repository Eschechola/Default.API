using Otanimes.Domain.Interfaces;
using Otanimes.Domain.Validators;

namespace Otanimes.Domain.Entities;

public class Contact : Entity, IAggregateRoot
{
    protected Contact()
    {
    }
    
    public Contact(
        string email,
        string primaryPhone,
        string? secondaryPhone)
    {
        Email = email;
        PrimaryPhone = primaryPhone;
        SecondaryPhone = secondaryPhone;

        Validate();
    }

    public string Email { get; private set; }
    public string PrimaryPhone { get; private set; }
    public string? SecondaryPhone { get; private set; }
    
    public void Validate()
        => base.Validate(new ContactValidator(), this);
}
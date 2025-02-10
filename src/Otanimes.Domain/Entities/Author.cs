using System;
using Otanimes.Domain.Interfaces;
using Otanimes.Domain.Validators;

namespace Otanimes.Domain.Entities;

public class Author : Entity, IAggregateRoot
{
    // EF
    public Address Address { get; private set; }
    public Contact Contact { get; private set; }

    protected Author()
    {
    }

    public Author(
        Guid addressId,
        Guid contactId,
        string firstName,
        string lastName)
    {
        AddressId = addressId;
        ContactId = contactId;
        FirstName = firstName;
        LastName = lastName;
        
        Validate();
    }

    public Guid AddressId { get; private set; }
    public Guid ContactId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public void Validate()
        => base.Validate(new AuthorValidator(), this);
}
using Otanimes.Domain.Interfaces;
using Otanimes.Domain.Validators;

namespace Otanimes.Domain.Entities;

public class Address : Entity, IAggregateRoot
{
    protected Address()
    {
    }
    
    public Address(
        string zipCode,
        string state,
        string city,
        string street,
        string number,
        string? complement)
    {
        ZipCode = zipCode;
        State = state;
        City = city;
        Street = street;
        Number = number;
        Complement = complement;

        Validate();
    }

    public string ZipCode { get; private set; }
    public string State { get; private set; }
    public string City { get; private set; }
    public string Street { get; private set; }
    public string Number { get; private set; }
    public string? Complement { get; private set; }

    public void Validate()
        => base.Validate(new AddressValidator(), this);
}
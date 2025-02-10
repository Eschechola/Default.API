using System;
using Otanimes.Domain.Interfaces;
using Otanimes.Domain.Validators;

namespace Otanimes.Domain.Entities;

public class User : Entity, IAggregateRoot
{
    public string Username { get; private set; }
    public string Password { get; private set; }
    public DateTime? LastLoginDate { get; private set; }

    protected User()
    {
    }
    
    public User(string username, string password)
    {
        Username = username;
        Password = password;

        Validate();
    }

    public void Validate()
        => base.Validate(new UserValidator(), this);

    public void LastLoginAtNow()
        => LastLoginDate = DateTime.UtcNow;
}
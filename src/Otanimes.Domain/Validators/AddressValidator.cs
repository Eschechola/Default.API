using FluentValidation;
using Otanimes.Domain.Entities;

namespace Otanimes.Domain.Validators;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(entity => entity.ZipCode)
            .NotNull()
            .NotEmpty()
            .Length(8);

        RuleFor(entity => entity.State)
            .NotNull()
            .NotEmpty()
            .Length(2);
        
        RuleFor(entity => entity.City)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);
        
        RuleFor(entity => entity.Street)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);
        
        RuleFor(entity => entity.Number)
            .NotNull()
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(15);
        
        RuleFor(entity => entity.Complement)
            .MaximumLength(500);
    }
}
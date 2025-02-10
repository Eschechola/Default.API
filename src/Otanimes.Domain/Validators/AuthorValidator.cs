using FluentValidation;
using Otanimes.Domain.Entities;

namespace Otanimes.Domain.Validators;

public class AuthorValidator : AbstractValidator<Author>
{
    public AuthorValidator()
    {
        RuleFor(entity => entity.FirstName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);
        
        RuleFor(entity => entity.LastName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);
    }
}
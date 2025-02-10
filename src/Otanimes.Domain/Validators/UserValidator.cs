using FluentValidation;
using Otanimes.Domain.Entities;

namespace Otanimes.Domain.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(entity => entity.Username)
            .NotEmpty()
            .NotNull()
            .MinimumLength(4)
            .MaximumLength(100);
    }
}
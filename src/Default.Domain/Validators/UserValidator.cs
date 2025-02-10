using Default.Domain.Entities;
using FluentValidation;

namespace Default.Domain.Validators;

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
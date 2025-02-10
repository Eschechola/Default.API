using FluentValidation;
using Otanimes.Domain.Entities;

namespace Otanimes.Domain.Validators;

public class ContactValidator : AbstractValidator<Contact>
{
    public ContactValidator()
    {
        RuleFor(entity => entity.Email)
            .NotNull()
            .NotEmpty()
            .MaximumLength(200)
            .EmailAddress();

        RuleFor(entity => entity.PrimaryPhone)
            .NotNull()
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(15);

        RuleFor(entity => entity.SecondaryPhone)
            .NotNull()
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(15)
            .When(entity=> entity.SecondaryPhone is not null);
    }
}
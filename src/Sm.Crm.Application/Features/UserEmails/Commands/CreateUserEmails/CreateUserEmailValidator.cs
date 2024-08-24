using FluentValidation;

namespace Sm.Crm.Application.Features.UserEmails.Commands.CreateUserEmails;

public class CreateUserEmailValidator : AbstractValidator<CreateUserEmailCommand>
{
    public CreateUserEmailValidator()
    {
        RuleFor(x => x.EmailAddress).NotEmpty().MaximumLength(250);
    }
}
using FluentValidation;

namespace Sm.Crm.Application.Features.Titles.Commands.CreateTitle;

public class CreateTitleValidator : AbstractValidator<CreateTitleCommand>
{
    public CreateTitleValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(250)
            .NotNull().WithMessage("Please enter a Title");
    }
}
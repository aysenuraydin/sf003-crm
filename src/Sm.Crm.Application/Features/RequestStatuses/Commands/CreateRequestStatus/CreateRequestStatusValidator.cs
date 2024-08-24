using FluentValidation;

namespace Sm.Crm.Application.Features.RequestStatuses.Commands.CreateRequestStatus;

public class CreateRequestStatusValidator : AbstractValidator<CreateRequestStatusCommand>
{
    public CreateRequestStatusValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
    }
}
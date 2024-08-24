using FluentValidation;

namespace Sm.Crm.Application.Features.DocumentTypes.Commands.CreateDocumentType;

public class CreateDocumentTypeValidator : AbstractValidator<CreateDocumentTypeCommand>
{
    public CreateDocumentTypeValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
    }
}
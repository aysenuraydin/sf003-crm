using FluentValidation;

namespace Sm.Crm.Application.Features.Territories.Commands.CreateTerritory;

public class CreateTerritoryValidator : AbstractValidator<CreateTerritoryCommand>
{
    public CreateTerritoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
    }
}
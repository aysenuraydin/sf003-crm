using FluentValidation;

namespace Sm.Crm.Application.Features.Territories.Commands.UptadeTerritory;

public class UpdateTerritoryValidator : AbstractValidator<UpdateTerritoryCommand>
{
    public UpdateTerritoryValidator()
    {
        RuleFor(x => x.RegionId).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
    }
}
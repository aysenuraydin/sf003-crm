using AutoMapper;
using Sm.Crm.Application.Features.Territories.Commands.CreateTerritory;
using Sm.Crm.Application.Features.Territories.Commands.UptadeTerritory;
using Sm.Crm.Application.Features.Territories.Queries;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Application.Features.Territories;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Territory, TerritoryDto>().ReverseMap();
        CreateMap<Territory, CreateTerritoryCommand>().ReverseMap();
        CreateMap<Territory, UpdateTerritoryCommand>().ReverseMap();
    }
}
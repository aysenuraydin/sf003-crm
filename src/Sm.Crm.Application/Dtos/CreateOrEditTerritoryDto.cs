using AutoMapper;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Application.Dtos;

public class CreateOrEditTerritoryDto
{
    public int TerritoryId { get; set; }
    public int TerritoryDescription { get; set; }
    public int RegionId { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Territory, CreateOrEditTerritoryDto>().ReverseMap();
        }
    }
}
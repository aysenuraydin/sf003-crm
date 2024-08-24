using AutoMapper;
using Sm.Crm.Application.Common.Mapping;
using Sm.Crm.Application.Features.DocumentTypes.Commands.CreateDocumentType;
using Sm.Crm.Application.Features.DocumentTypes.Commands.UpdateDocumentType;
using Sm.Crm.Application.Features.DocumentTypes.Queries;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Application.Features.DocumentTypes;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<DocumentType, CreateDocumentTypeCommand>().ReverseMap();
        CreateMap<DocumentType, UpdateDocumentTypeCommand>().ReverseMap();
        CreateMap<DocumentType, DocumentTypeDto>();
        CreateMap<string, DateOnly>().ConvertUsing(new DateTimeTypeConverter());
    }
}
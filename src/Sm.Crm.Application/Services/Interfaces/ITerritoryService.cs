using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;

namespace Sm.Crm.Application.Services.Interfaces;

public interface ITerritoryService
{
    Task<Result<List<TerritoryDto>>> GetAllAsync();

    Task<PaginatedResult<TerritoryDto>> GetPaginatedAsync(PaginationRequest request);

    Task<Result<TerritoryDto?>> GetByIdAsync(int id);

    Task<Result<int>> CreateAsync(CreateOrEditTerritoryDto dto);

    Task<Result<bool>> UpdateAsync(CreateOrEditTerritoryDto dto);

    Task<Result<bool>> DeleteAsync(int id);
}
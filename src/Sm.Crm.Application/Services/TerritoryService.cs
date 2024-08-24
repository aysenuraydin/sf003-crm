using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Services.Interfaces;
using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Application.Services;

public class TerritoryService : ITerritoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TerritoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<TerritoryDto>>> GetAllAsync()
    {
        var entities = await _unitOfWork.TerritoryRepository.GetAll().ToListAsync();
        return Result<List<TerritoryDto>>.Success(_mapper.Map<List<TerritoryDto>>(entities).ToList());
    }

    public async Task<PaginatedResult<TerritoryDto>> GetPaginatedAsync(PaginationRequest request)
    {
        var entities = _unitOfWork.SaleRepository.GetAll()
            //.Include(e => e.EmployeeUserFk)
            .OrderByDescending(e => e.Id)
            .ProjectTo<TerritoryDto>(_mapper.ConfigurationProvider);

        return await PaginatedResult<TerritoryDto>.Create(entities.AsNoTracking(), request.PageNumber, request.PageSize);
    }

    public async Task<Result<TerritoryDto?>> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.TerritoryRepository.GetAll().FirstOrDefaultAsync(e => e.Id == id);
        return Result<TerritoryDto?>.Success(_mapper.Map<TerritoryDto>(entity));
    }

    public async Task<Result<int>> CreateAsync(CreateOrEditTerritoryDto dto)
    {
        var entity = _mapper.Map<Territory>(dto);
        var id = await _unitOfWork.TerritoryRepository.Create(entity);
        await _unitOfWork.CommitAsync();
        return Result<int>.Success(id);
    }

    public async Task<Result<bool>> UpdateAsync(CreateOrEditTerritoryDto dto)
    {
        var entity = _mapper.Map<Territory>(dto);
        bool isSuccess = await _unitOfWork.TerritoryRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Updated!");
        else
            return Result<bool>.Failure("Not updated!");
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        bool isSuccess = await _unitOfWork.TerritoryRepository.DeleteById(id);
        await _unitOfWork.CommitAsync();
        if (isSuccess)
            return Result<bool>.Success("Deleted!");
        else
            return Result<bool>.Failure("Not deleted!");
    }
}
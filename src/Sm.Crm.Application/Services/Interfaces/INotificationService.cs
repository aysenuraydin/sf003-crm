using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;

namespace Sm.Crm.Application.Services.Interfaces;

public interface INotificationService
{
    Task<Result<List<NotificationDto>>> GetAll();

    Task<PaginatedResult<NotificationDto>> GetPaginated(PaginationRequest req);

    Task<Result<NotificationDto?>> GetById(int id);

    Task<Result<int>> Create(CreateOrEditNotificationDto userAddress);

    Task<Result<bool>> Update(CreateOrEditNotificationDto userAddress);

    Task<Result<bool>> Delete(int id);
}
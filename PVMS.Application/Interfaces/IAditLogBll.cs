using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;

namespace PVMS.Application.Interfaces
{
    public interface IAditLogBll
    {
        Task<AditLog?> GetByIdAsync(Guid id);
        Task<PageResult<AditLog>> GetAllAsync(AditLogFilter filter);
    }
}

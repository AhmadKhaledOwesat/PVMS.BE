using Microsoft.EntityFrameworkCore;
using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Infrastructure.EfContext;

namespace PVMS.Application.Bll
{
    public class AditLogBll(StudioContext context) : IAditLogBll
    {
        public async Task<AditLog?> GetByIdAsync(Guid id)
        {
            return await context.AditLogs
                .AsNoTracking()
                .Include(x => x.Creator)
                .Include(x => x.Modifier)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PageResult<AditLog>> GetAllAsync(AditLogFilter filter)
        {
            var query = context.AditLogs
                .AsNoTracking()
                .Include(x => x.Creator)
                .Include(x => x.Modifier)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.EntityType))
                query = query.Where(x => x.EntityType == filter.EntityType);
            if (filter.EntityId.HasValue)
                query = query.Where(x => x.EntityId == filter.EntityId);
            if (filter.CreatedBy.HasValue && !filter.CreatedBy.Value.Equals(Guid.Empty))
                query = query.Where(x => x.CreatedBy == filter.CreatedBy);
            if (filter.CreatedDateFrom.HasValue)
                query = query.Where(x => x.CreatedDate >= filter.CreatedDateFrom.Value);
            if (filter.CreatedDateTo.HasValue)
                query = query.Where(x => x.CreatedDate <= filter.CreatedDateTo.Value);

            var count = await query.CountAsync();

            var pageNumber = filter.PagingParameters?.PageNumber ?? 1;
            var pageSize = filter.PagingParameters?.PageSize ?? 10;
            var list = await query
                .OrderByDescending(x => x.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResult<AditLog> { Count = count, Collections = list };
        }
    }
}

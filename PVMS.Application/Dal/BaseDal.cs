using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;
using PVMS.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace PVMS.Application.Dal
{
    public class BaseDal<T, TId, TFilter>(IEfRepository<T, TId> efRepository, IIdentityManager<TId> identityManager) : IBaseDal<T, TId, TFilter> where T : BaseEntity<TId>
        where TId : struct
        where TFilter : SearchParameters<T>
    {
        public virtual async Task AddAsync(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            if (entity.CreatedBy.Equals(Guid.Empty))
                entity.CreatedBy = identityManager.CurrentUserId;
            await efRepository.InsertAsync(entity);
        }

        public virtual async Task<List<T>> GetAllAsync() => await efRepository.Table.ToListAsync();
        public virtual async Task<T> FindByExpressionAsync(Expression<Func<T, bool>> expression) => await efRepository.Table.FirstOrDefaultAsync(expression);
        public virtual async Task<T> FindLastByExpressionAsync(Expression<Func<T, bool>> expression) => await efRepository.Table.OrderByDescending(x => x.CreatedDate).LastOrDefaultAsync(expression);
        public virtual async Task<List<T>> FindAllByExpressionAsync(Expression<Func<T, bool>> expression) => await efRepository.Table.Where(expression).ToListAsync();
        public virtual async Task<int> GetCountByExpressionAsync(Expression<Func<T, bool>> expression) => await efRepository.Table.Where(expression).CountAsync();

        public virtual async Task<PageResult<T>> GetAllAsync(TFilter searchParameters)
        {
            var result = await GetAllAsync();

            if (searchParameters.Expression != null)
                result = [.. result.Where(searchParameters.Expression)];

            return new PageResult<T>
            {
                Collections = [.. result.Skip(((searchParameters.PagingParameters?.PageNumber ?? 1) - 1) * (searchParameters.PagingParameters?.PageSize ?? 10)).Take(searchParameters.PagingParameters?.PageSize ?? 10)],
                Count = result.Count,
            };
        }

        public virtual async Task<T> GetByIdAsync(TId id) => await efRepository.GetByIdAsync(id);

        public virtual async Task UpdateAsync(T entity)
        {
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = identityManager.CurrentUserId;
            await efRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteRangeAsync(List<T> entities)
        {
            await efRepository.DeleteRangeAsync(entities);
        }
        public virtual async Task<bool> DeleteAsync(TId id)
        {
            return await efRepository.DeleteAsync(id);
        }
        public virtual async Task UpdateRangeAsync(List<T> entities)
        {
            entities.ForEach(entity =>
            {
                entity.ModifiedDate = DateTime.Now;
                entity.ModifiedBy = identityManager.CurrentUserId;
            });
            await efRepository.UpdateRangeAsync(entities);
        }
        public virtual async Task AddRangeAsync(List<T> entities)
        {
            entities.ForEach(entity =>
            {
                entity.CreatedDate = DateTime.Now;
                if (entity.CreatedBy.Equals(Guid.Empty))
                    entity.CreatedBy = identityManager.CurrentUserId;
            });
            await efRepository.InsertRangeAsync(entities);
        }

        public virtual async Task<dynamic> ExecuteSQL(string qry) => await efRepository.ExecuteSQL(qry);
    }
}

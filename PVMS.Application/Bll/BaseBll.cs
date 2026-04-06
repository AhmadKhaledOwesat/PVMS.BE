using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;
using System.Linq.Expressions;

namespace PVMS.Application.Bll
{
    public partial class BaseBll<T,TId,TFilter>(IBaseDal<T,TId,TFilter> baseDal):IBaseBll<T,TId,TFilter> where T : BaseEntity<TId> where TId : struct
    {
        public virtual async Task AddAsync(T entity) => await baseDal.AddAsync(entity);
        public virtual async Task<List<T>> GetAllAsync()=>await  baseDal.GetAllAsync();
        public virtual async Task<PageResult<T>> GetAllAsync(TFilter searchParameters)=>await baseDal.GetAllAsync(searchParameters);    
        public virtual async Task<T> GetByIdAsync(TId id)=> await baseDal.GetByIdAsync(id);
        public virtual async Task UpdateAsync(T entity)=>  await baseDal.UpdateAsync(entity);
        public virtual async Task<List<T>> FindAllByExpressionAsync(Expression<Func<T, bool>> expression) => await baseDal.FindAllByExpressionAsync(expression);
        public virtual async Task<T> FindByExpressionAsync(Expression<Func<T, bool>> expression) => await baseDal.FindByExpressionAsync(expression);
        public virtual async Task<T> FindLastByExpressionAsync(Expression<Func<T, bool>> expression) => await baseDal.FindLastByExpressionAsync(expression);
        public virtual async Task DeleteRangeAsync(List<T> entities) => await baseDal.DeleteRangeAsync(entities);
        public virtual async Task AddRangeAsync(List<T> entities) => await baseDal.AddRangeAsync(entities);
        public virtual async Task UpdateRangeAsync(List<T> entities) => await baseDal.UpdateRangeAsync(entities);
        public virtual async Task<bool> DeleteAsync(TId id) => await baseDal.DeleteAsync(id);
        public virtual async Task<int> GetCountByExpressionAsync(Expression<Func<T, bool>> expression) => await baseDal.GetCountByExpressionAsync(expression);
    }
}

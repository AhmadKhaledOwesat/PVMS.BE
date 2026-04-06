using PVMS.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace PVMS.Infrastructure.Repositories
{
    public partial interface IEfRepository<T, TId> where T : BaseEntity<TId>
        where TId : struct
    {
        Task<T> GetByIdAsync(TId id);
        Task InsertAsync(T entity);
        Task InsertRangeAsync(List<T> entities);
        Task UpdateAsync(T entity);
        Task<bool> DeleteAsync(TId id);
        IQueryable<T> Table { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task UpdateRangeAsync(List<T> entities);
        Task DeleteRangeAsync(List<T> entities);
        Task<dynamic> ExecuteSQL(string qry);
        Task<List<int>> ExecuteAsync(string qry);
        Task<int> ExecuteCommandAsync(string query);
        Task<int> GetCountAsync(Expression<Func<T, bool>> expression);
    }
}

using PVMS.Domain.Entities;
using PVMS.Infrastructure.EfContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Dynamic;
using System.Linq.Expressions;
namespace PVMS.Infrastructure.Repositories
{
    public partial class EfRepository<T, TId>(StudioContext context) : IEfRepository<T, TId> where T : BaseEntity<TId>
        where TId : struct
    {
        private DbSet<T> _entities;

        public async Task<T> GetByIdAsync(TId id) => await Entities!.FindAsync(id);
        public async Task InsertAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await Entities.AddAsync(entity);
            await context.SaveChangesAsync();
        }
        public async Task InsertRangeAsync(List<T> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);
            await Entities.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }
        public async Task DeleteRangeAsync(List<T> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);
            Entities.RemoveRange(entities);
            await context.SaveChangesAsync();
        }
        public async Task<int> GetCountAsync(Expression<Func<T, bool>> expression)
        {
            return await Entities.CountAsync(expression);
        }
        public async Task UpdateAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            // Load the existing entity from database to preserve original values
            var existingEntity = await Entities.FindAsync(entity.Id);
            if (existingEntity == null)
            {
                throw new InvalidOperationException($"Entity with id {entity.Id} not found");
            }

            // Copy the modified values to the tracked entity
            context.Entry(existingEntity).CurrentValues.SetValues(entity);

            // Ensure the entity is marked as modified
            context.Entry(existingEntity).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }
        public async Task UpdateRangeAsync(List<T> entities)
        {
            entities.ForEach((entity) =>
            {
                context.Entry(entity).State = EntityState.Modified;
            });

            context.UpdateRange(entities);
            await context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(TId id)
        {
            T entity = await GetByIdAsync(id);
            Entities.Remove(entity);
            return (await context.SaveChangesAsync()) > 0;
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync() => context.Database.CurrentTransaction ?? await context.Database.BeginTransactionAsync();
        public async Task<dynamic> ExecuteSQL(string qry)
        {
            var list = new List<dynamic>();
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = qry;
                command.CommandType = CommandType.Text;
                await context.Database.OpenConnectionAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    var names = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                    foreach (IDataRecord record in reader)
                    {
                        var expando = new ExpandoObject() as IDictionary<string, dynamic>;
                        foreach (var name in names)
#pragma warning disable CS8601 // Possible null reference assignment.
                            expando[name] = record[name] == DBNull.Value ? null : record[name];
#pragma warning restore CS8601 // Possible null reference assignment.

                        list.Add(expando);
                    }
                }
                await context.Database.CloseConnectionAsync();
            }
            return list;
        }
        public async Task<List<int>> ExecuteAsync(string qry)
        {
            var list = new List<int>();
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = qry;
                command.CommandType = CommandType.Text;
                await context.Database.OpenConnectionAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    var names = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                    foreach (IDataRecord record in reader)
                    {
                        foreach (var name in names)
                            list.Add(record[name] == DBNull.Value ? 0 : int.Parse(record[name]!.ToString()!));
                    }
                }
                await context.Database.CloseConnectionAsync();
            }
            return list;
        }
        public async Task<int> ExecuteCommandAsync(string query) => await context.Database.ExecuteSqlRawAsync(query);

        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }
        private DbSet<T> Entities
        {
            get
            {
                _entities ??= context.Set<T>();
                return _entities;
            }
        }
    }
}

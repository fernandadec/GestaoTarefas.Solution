using GestaoTarefas.Domain.Entities.Base;
using GestaoTarefas.Domain.Entities.Comum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace GestaoTarefas.Domain.Interfaces.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> AtivarDesativarAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<T> SelectAsync(Guid id);
        Task<IEnumerable<T>> SelectAsync();
        Task<bool> IsExists(Guid id);
        IQueryable<T> ListNoTracking(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultNoTrackingAsync(Expression<Func<T, bool>> predicate);
        DbSet<T> DataSet { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
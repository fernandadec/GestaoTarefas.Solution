using Microsoft.EntityFrameworkCore.Storage;

namespace GestaoTarefas.Domain.Interfaces.DataModule
{
    public interface IDataModule
    {
        IDbContextTransaction CurrentTransaction { get; set; }
        Task StartTransactionAsync();
        Task CommitDataAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
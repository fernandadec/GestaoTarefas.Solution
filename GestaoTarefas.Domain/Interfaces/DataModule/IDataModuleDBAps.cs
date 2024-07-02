using GestaoTarefas.Domain.Entities.Comum;
using GestaoTarefas.Domain.Interfaces.Repository;

namespace GestaoTarefas.Domain.Interfaces.DataModule
{
    public interface IDataModuleDBAps : IDataModule
    {
        IRepository<ListaEntity> ListaRepository { get; }
        IRepository<ListaTarefasEntity> ListaTarefasRepository { get; }
        IRepository<UsuarioEntity> UsuarioRepository { get; }

    }
}

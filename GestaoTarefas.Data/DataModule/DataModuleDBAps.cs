using GestaoTarefas.Data.Context;
using GestaoTarefas.Data.Repository;
using GestaoTarefas.Domain.Entities.Comum;
using GestaoTarefas.Domain.Interfaces.DataModule;
using GestaoTarefas.Domain.Interfaces.Repository;

namespace GestaoTarefas.Data.DataModule
{
    public class DataModuleDBAps : DataModule<ApsContext>, IDataModuleDBAps
    {
        public DataModuleDBAps(ApsContext dbContext)
        : base(dbContext) { }


        public IRepository<ListaEntity> _listaRepository = null;
        public IRepository<ListaEntity> ListaRepository
        {
            get => _listaRepository ??= new BaseRepository<ListaEntity>(CurrentContext);
        }

        public IRepository<ListaTarefasEntity> _listaTarefasRepository = null;
        public IRepository<ListaTarefasEntity> ListaTarefasRepository
        {
            get => _listaTarefasRepository ??= new BaseRepository<ListaTarefasEntity>(CurrentContext);
        }

        public IRepository<UsuarioEntity> _usuarioRepository = null;
        public IRepository<UsuarioEntity> UsuarioRepository
        {
            get => _usuarioRepository ??= new BaseRepository<UsuarioEntity>(CurrentContext);
        }
    }
}
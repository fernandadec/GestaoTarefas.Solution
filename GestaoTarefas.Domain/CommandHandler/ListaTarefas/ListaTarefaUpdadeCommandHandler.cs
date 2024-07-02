using AutoMapper;
using FluentValidation;
using GestaoTarefas.Domain.Command.Lista;
using GestaoTarefas.Domain.Command.ListaTarefas;
using GestaoTarefas.Domain.CommandHandler.Base;
using GestaoTarefas.Domain.Entities.Comum;
using GestaoTarefas.Domain.Interfaces.DataModule;
using Microsoft.EntityFrameworkCore;

namespace GestaoTarefas.Domain.CommandHandler.ListaTarefas
{
    public class ListaTarefaUpdadeCommandHandler : UpdateCommandHandlerBase<ListaTarefaUpdateCommand, ListaTarefasEntity>
    {
        public ListaTarefaUpdadeCommandHandler(
            IDataModuleDBAps dataModule,
        IMapper mapper,
            IValidator<ListaTarefaUpdateCommand> validator)
        : base(dataModule,
              mapper,
              validator,
              dataModule.ListaTarefasRepository)
        {
            OnRequestRepositoryData += async (ListaTarefaUpdateCommand request) =>
            {
                var listaEntity = await dataModule.ListaTarefasRepository.DataSet
                    .FirstOrDefaultAsync(x => x.Id.Equals(request.id))
                    ?? throw new ArgumentException("Lista não localizada.");

                //// Mapeia os dados da requisição para a entidade de banco de dados  
                //listaEntity.NomeLista = request.NomeLista;
                //listaEntity.NomeAutor = request.NomeAutor;

                // Atualiza a lista no repositório
                await dataModule.ListaTarefasRepository.UpdateAsync(listaEntity);

                return listaEntity;
            };
        }
    }
}
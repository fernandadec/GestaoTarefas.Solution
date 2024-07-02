using AutoMapper;
using FluentValidation;
using GestaoTarefas.Domain.Command.Lista;
using GestaoTarefas.Domain.CommandHandler.Base;
using GestaoTarefas.Domain.Entities.Comum;
using GestaoTarefas.Domain.Interfaces.DataModule;
using Microsoft.EntityFrameworkCore;

namespace GestaoTarefas.Domain.CommandHandler.Lista
{
    public class ListaDeleteCommandHandler : DeleteCommandHandlerBase<ListaDeleteCommand, ListaEntity>
    {
        public ListaDeleteCommandHandler(
            IDataModuleDBAps dataModule,
            IMapper mapper,
            IValidator<ListaDeleteCommand> validator)
        : base(dataModule, mapper, validator, dataModule.ListaRepository) { }

        public override async Task<bool> Handle(ListaDeleteCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await Repository.BeginTransactionAsync())
            {
                try
                {
                    var lista = await Repository.DataSet
                        .Include(l => l.ListaTarefas)
                        .FirstOrDefaultAsync(x => x.Id.Equals(request.IdLista))
                        ?? throw new ArgumentException("Lista não localizada.");

                    if (lista.ListaTarefas != null && lista.ListaTarefas.Any())
                    {
                        foreach (var tarefa in lista.ListaTarefas.ToList()) 
                        {
                            await Repository.DeleteAsync(tarefa.Id);
                        }
                    }

                    await Repository.DeleteAsync(lista.Id);

                    await transaction.CommitAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new ArgumentException("Erro ao excluir lista e suas tarefas vinculadas: " + ex.Message);
                }
            }
        }
    }
}




using FluentValidation;
using GestaoTarefas.Domain.Command.Lista;
using GestaoTarefas.Domain.Interfaces.DataModule;
using GestaoTarefas.Domain.Validator.Base;
using Microsoft.EntityFrameworkCore;

namespace GestaoTarefas.Domain.Validator.Lista
{
    public class ListaDeleteValidator : BaseValidator<ListaDeleteCommand>
    {
        public ListaDeleteValidator(IDataModuleDBAps dataModuleDBAps)
        {
            RuleFor(x => x).MustAsync(async (command, cancellation) => await Existe(dataModuleDBAps, command)).WithMessage("Id Lista não encontrado.");
            RuleFor(x => x).MustAsync(async (command, cancellation) => await PossuiTarefa(dataModuleDBAps, command)).WithMessage("Não é possivel deletar , possui tarefas.");

        }
        private async Task<bool> Existe(IDataModuleDBAps dataModuleDBAps, ListaDeleteCommand command)
        {
            var result = await dataModuleDBAps.ListaRepository.DataSet
                .Where(x => x.Id.Equals(command.IdLista))
                .ToListAsync();

            return result.Count() > 0;
        }

        private async Task<bool> PossuiTarefa(IDataModuleDBAps dataModuleDBAps, ListaDeleteCommand command)
        {
            var lista = await dataModuleDBAps.ListaRepository.DataSet
                .Include(l => l.ListaTarefas)
                .FirstOrDefaultAsync(x => x.Id.Equals(command.IdLista));

            if(lista!=null)
                return lista.ListaTarefas.Count() ==0;

            return false;
        }
    }
}

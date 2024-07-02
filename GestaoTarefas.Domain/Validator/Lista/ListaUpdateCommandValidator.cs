using FluentValidation;
using GestaoTarefas.Domain.Command.Lista;
using GestaoTarefas.Domain.Interfaces.DataModule;
using GestaoTarefas.Domain.Validator.Base;
using Microsoft.EntityFrameworkCore;

namespace GestaoTarefas.Domain.Validator.Lista
{
    public class ListaUpdateCommandValidator : BaseValidator<ListaUpdateCommand>
    {
        public ListaUpdateCommandValidator(IDataModuleDBAps dataModuleDBAps)
        {
            RuleFor(x => x.NomeLista).NotEmpty().WithMessage("Informe o nome da lista.");
            RuleFor(x => x.NomeAutor).NotEmpty().WithMessage("Informe o nome do autor.");
            RuleFor(x => x).MustAsync(async (command, cancellation) => await IsUniqueNomeLista(dataModuleDBAps, command)).WithMessage("Já existe uma lista com esse nome.");
        }
        private async Task<bool> IsUniqueNomeLista(IDataModuleDBAps dataModuleDBAps, ListaUpdateCommand command)
        {
            var result = await dataModuleDBAps.ListaRepository.DataSet
                .Where(x => x.NomeLista.Equals(command.NomeLista))
                .ToListAsync();

            return result.Count()==0;
        }
    }
}


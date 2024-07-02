using GestaoTarefas.Domain.Command.Lista;
using GestaoTarefas.Domain.Interfaces.DataModule;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using GestaoTarefas.Domain.Validator.Base;

namespace GestaoTarefas.Domain.Validator.Lista
{
    public class ListaCreateValidator : BaseValidator<ListaCreateCommand>
    {
        public ListaCreateValidator(IDataModuleDBAps dataModuleDBAps)
        {
            RuleFor(x => x.NomeLista).NotEmpty().WithMessage("Informe o nome da lista.");
            RuleFor(x => x.NomeAutor).NotEmpty().WithMessage("Informe o nome do autor.");
            RuleFor(x => x).MustAsync(async (command, cancellation) => await IsUnique(dataModuleDBAps, command)).WithMessage("Já existe uma lista com esse código.");
            RuleFor(x => x).MustAsync(async (command, cancellation) => await IsUniqueNomeLista(dataModuleDBAps, command)).WithMessage("Já existe uma lista com esse nome.");

        }
        private async Task<bool> IsUnique(IDataModuleDBAps dataModuleDBAps, ListaCreateCommand command)
        {
            var result = await dataModuleDBAps.ListaRepository.DataSet
                .Where(x => x.Id.Equals(command.IdLista))
                .ToListAsync();

            return result.Count().Equals(0);
        }
        private async Task<bool> IsUniqueNomeLista(IDataModuleDBAps dataModuleDBAps, ListaCreateCommand command)
        {
            var result = await dataModuleDBAps.ListaRepository.DataSet
                .Where(x => x.NomeLista.Equals(command.NomeLista))
                .ToListAsync();

            return result.Count().Equals(0);
        }
    }
}


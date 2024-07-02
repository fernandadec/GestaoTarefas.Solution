using FluentValidation;
using GestaoTarefas.Domain.Command.Lista;
using GestaoTarefas.Domain.Interfaces.DataModule;
using GestaoTarefas.Domain.Query.Lista;
using GestaoTarefas.Domain.Validator.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefas.Domain.Validator.ListaTarefa
{
    public class ListaTarefasByIdQueryValidator : BaseValidator<ListaTarefasByIdQuery>
    {
        public ListaTarefasByIdQueryValidator(IDataModuleDBAps dataModuleDBAps)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Informe o id da lista.");
        }
    }
}
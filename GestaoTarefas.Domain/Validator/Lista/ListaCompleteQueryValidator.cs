using GestaoTarefas.Domain.Interfaces.DataModule;
using GestaoTarefas.Domain.Query.Lista;
using GestaoTarefas.Domain.Validator.Base;

namespace GestaoTarefas.Domain.Validator.Lista
{
    public class ListaCompleteQueryValidator : BaseValidator<ListaCompleteQuery>
    {
        public ListaCompleteQueryValidator(IDataModuleDBAps dataModuleDBAps)
        {


        }
    }
}

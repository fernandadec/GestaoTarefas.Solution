using MediatR;

namespace GestaoTarefas.Domain.Command.Lista
{
    public class ListaDeleteCommand : IRequest<bool>
    {
        public Guid IdLista { get; set; }
    }
}

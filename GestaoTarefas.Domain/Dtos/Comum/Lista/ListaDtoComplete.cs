

namespace GestaoTarefas.Domain.Dtos.Comum.Lista
{
    public class ListaDtoComplete
    {
        public Guid? IdLista { get; set; }
        public string NomeLista { get; set; }
        public string NomeAutor { get; set; }
        public DateTime DataCriacao { get; set; }

        public IEnumerable<ListaTarefaDto> ListaTarefas { get; set; }
    }
}

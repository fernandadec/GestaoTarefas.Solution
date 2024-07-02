
namespace GestaoTarefas.Domain.Dtos.Comum.Lista
{
    public class ListaDto
    {
        public Guid? IdLista { get; set; }
        public string NomeLista { get; set; }
        public string NomeAutor { get; set; }
        public DateTime DataCriacao { get; set; }

        public IEnumerable<ListaTarefaDto> ListaTarefas { get; set; }

    }
}

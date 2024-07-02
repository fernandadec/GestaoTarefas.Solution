using GestaoTarefas.Web.Models.ListaTarefa;

namespace GestaoTarefas.Web.Models.Lista
{
    public class ListaModel
    {
        public Guid? IdLista { get; set; }
        public string NomeLista { get; set; }
        public string NomeAutor { get; set; }
        public DateTime DataCriacao { get; set; }

        public IEnumerable<ListaTarefaModel> ListaTarefas { get; set; } = new List<ListaTarefaModel>();
    }
}

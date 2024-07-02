

namespace GestaoTarefas.Domain.Query.Lista
{
    public class ListaTarefaQuery
    {
        public Guid? IdListaTarefa { get; set; }
        public Guid ListaId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}

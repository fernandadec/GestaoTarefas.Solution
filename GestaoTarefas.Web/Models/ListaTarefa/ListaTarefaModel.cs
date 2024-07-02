namespace GestaoTarefas.Web.Models.ListaTarefa
{
    public class ListaTarefaModel
    {
        public Guid? id { get; set; }
        public Guid ListaId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}

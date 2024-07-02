using GestaoTarefas.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoTarefas.Domain.Entities.Comum
{
    public class ListaEntity : BaseEntity
    {
        public string NomeLista { get; set; }
        public string NomeAutor { get; set; }
        public DateTime DataCriacao { get; set; }
        public IEnumerable<ListaTarefasEntity> ListaTarefas { get; set; }
        [Column("idLista")] 
        public override Guid Id { get; set; }

    }
}

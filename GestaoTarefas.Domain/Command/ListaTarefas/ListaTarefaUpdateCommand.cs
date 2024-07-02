﻿using MediatR;

namespace GestaoTarefas.Domain.Command.ListaTarefas
{
    public class ListaTarefaUpdateCommand : IRequest<bool>
    {
        public Guid id { get; set; }
        public Guid ListaId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}

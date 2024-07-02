using AutoMapper;
using GestaoTarefas.Domain.Dtos.Comum.Lista;
using GestaoTarefas.Domain.Interfaces.DataModule;
using GestaoTarefas.Domain.Query.Lista;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GestaoTarefas.Domain.QueryHandler.Lista
{
    public class ListaTarefasByIdQueryHandler : IRequestHandler<ListaTarefasByIdQuery, List<ListaDtoComplete>>
    {
        private readonly IDataModuleDBAps _dataModule;
        private readonly IMapper _mapper;

        public ListaTarefasByIdQueryHandler(IDataModuleDBAps dataModule, IMapper mapper)
        {
            _dataModule = dataModule;
            _mapper = mapper;
        }
        public async Task<List<ListaDtoComplete>> Handle(ListaTarefasByIdQuery request, CancellationToken cancellationToken)
        {
            var listaId = request.Id;

            var listaEntity = _dataModule.ListaRepository
                                        .ListNoTracking(x => x.Id == listaId)
                                        .Include(l => l.ListaTarefas) 
                                        .FirstOrDefault();

            if (listaEntity == null)
            {
                return null; 
            }

            var listaDto = _mapper.Map<ListaDtoComplete>(listaEntity);


            var listaDtoList = new List<ListaDtoComplete> { listaDto };

            return listaDtoList; 
        }
    }
}
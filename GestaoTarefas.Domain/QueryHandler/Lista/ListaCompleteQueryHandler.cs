using AutoMapper;
using GestaoTarefas.Domain.Dtos.Comum.Lista;
using GestaoTarefas.Domain.Interfaces.DataModule;
using GestaoTarefas.Domain.Query.Lista;
using MediatR;

namespace GestaoTarefas.Domain.QueryHandler.Lista
{
    public class ListaCompleteQueryHandler : IRequestHandler<ListaCompleteQuery, List<ListaDtoComplete>>
    {
        private readonly IDataModuleDBAps _dataModule;
        private readonly IMapper _mapper;

        public ListaCompleteQueryHandler(IDataModuleDBAps dataModule, IMapper mapper)
        {
            _dataModule = dataModule;
            _mapper = mapper;
        }

        public Task<List<ListaDtoComplete>> Handle(ListaCompleteQuery request, CancellationToken cancellationToken)
        {
            var result = _dataModule.ListaRepository.ListNoTracking(x => true);

            return Task.FromResult(_mapper.Map<List<ListaDtoComplete>>(result));
        }
    }
}

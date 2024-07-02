using AutoMapper;
using FluentValidation;
using GestaoTarefas.Domain.Command.Lista;
using GestaoTarefas.Domain.CommandHandler.Base;
using GestaoTarefas.Domain.Entities.Comum;
using GestaoTarefas.Domain.Interfaces.DataModule;
using Microsoft.EntityFrameworkCore;

namespace GestaoTarefas.Domain.CommandHandler.Lista
{
    public class ListaUpdateCommandHandler
        : UpdateCommandHandlerBase<ListaUpdateCommand, ListaEntity>
    {
        public ListaUpdateCommandHandler(
            IDataModuleDBAps dataModule,
            IMapper mapper,
            IValidator<ListaUpdateCommand> validator)
        : base(dataModule,
              mapper,
              validator,
              dataModule.ListaRepository)
        {
            OnRequestRepositoryData += async (ListaUpdateCommand request) =>
            {
                var listaEntity = await dataModule.ListaRepository.DataSet
                    .Include(l => l.ListaTarefas)
                    .FirstOrDefaultAsync(x => x.Id.Equals(request.IdLista) || x.NomeLista.Equals(request.NomeLista))
                     ?? throw new ArgumentException("Lista não localizada.");

                var obj = mapper.Map<ListaEntity>(request);


                obj.Id=request.IdLista;
                obj.NomeLista = request.NomeLista;
                obj.NomeAutor = request.NomeAutor;

                foreach (var _tarefa in request.ListaTarefas)
                {
                    var tarefaEntity = listaEntity.ListaTarefas.FirstOrDefault(t => t.Id == _tarefa.id);
                    if (tarefaEntity != null)
                    {
                        _tarefa.ListaId=request.IdLista;
                        mapper.Map(_tarefa, tarefaEntity);
                    }
                    else
                    {
                        var novaTarefaEntity = mapper.Map<ListaTarefasEntity>(_tarefa);
                        await dataModule.ListaTarefasRepository.InsertAsync(novaTarefaEntity);
                    }
                }

                var tarefasRemovidas = listaEntity.ListaTarefas.Where(t => !request.ListaTarefas.Any(rt => rt.id == t.Id)).ToList();
                foreach (var tarefaRemovida in tarefasRemovidas)
                {

                    await dataModule.ListaTarefasRepository.DeleteAsync(tarefaRemovida.Id);
                }

                await dataModule.ListaRepository.UpdateAsync(obj);

                return listaEntity;
            };
        }
    }
}
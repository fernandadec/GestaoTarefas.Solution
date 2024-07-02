using AutoMapper;
using GestaoTarefas.Domain.Command.Lista;
using GestaoTarefas.Domain.Command.ListaTarefas;
using GestaoTarefas.Domain.Dtos.Comum.Lista;
using GestaoTarefas.Domain.Entities.Comum;
using GestaoTarefas.Domain.Query.Lista;

namespace GestaoTarefas.CrossCutting.Mappings.Lista
{

    public class ListaProfile : Profile
    {
        public ListaProfile()
        {
            CreateMap<ListaEntity, ListaDtoComplete>()
                .ForMember(dest => dest.IdLista, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ListaTarefas, opt => opt.MapFrom(src => src.ListaTarefas.Select(t => new ListaTarefaDto
                {
                    id = t.Id,
                    Titulo = t.Titulo,
                    Descricao = t.Descricao,
                    DataCriacao = t.DataCriacao,
                    Ativo = t.Ativo,
                    ListaId = t.ListaId
                })));

            CreateMap<ListaUpdateCommand, ListaEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdLista))
                .ForMember(dest => dest.ListaTarefas, opt => opt.MapFrom(src => src.ListaTarefas));

            CreateMap<ListaTarefaUpdateCommand, ListaTarefasEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id));

            CreateMap<ListaDto, ListaCreateCommand>().ReverseMap();

            CreateMap<ListaEntity, ListaCreateCommand>().ReverseMap();

            CreateMap<ListaEntity, ListaQuery>().ReverseMap();

            CreateMap<ListaEntity, ListaCompleteQuery>().ReverseMap();

            CreateMap<ListaEntity, ListaDto>().ReverseMap();

            CreateMap<ListaTarefaCreateCommand, ListaTarefasEntity>();

            CreateMap<ListaTarefaDto, ListaTarefasEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id));

            CreateMap<ListaDeleteCommand, ListaEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdLista));

        }
    }
}


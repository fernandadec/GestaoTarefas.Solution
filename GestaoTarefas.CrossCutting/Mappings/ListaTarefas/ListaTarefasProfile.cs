using AutoMapper;
using GestaoTarefas.Domain.Command.Lista;
using GestaoTarefas.Domain.Command.ListaTarefas;
using GestaoTarefas.Domain.Dtos.Comum.Lista;
using GestaoTarefas.Domain.Entities.Comum;


namespace GestaoTarefas.CrossCutting.Mappings.ListaTarefas
{
    public class ListaTarefasProfile : Profile
    {
        public ListaTarefasProfile()
        {

            CreateMap<ListaTarefasEntity, ListaTarefaDto>();
            CreateMap<ListaTarefaUpdateCommand, ListaEntity>();
            CreateMap<ListaUpdateCommand, ListaEntity>()
    .ForMember(dest => dest.ListaTarefas, opt => opt.MapFrom(src => src.ListaTarefas));

            CreateMap<ListaTarefasEntity, ListaTarefaDto>().ReverseMap();
            CreateMap<ListaTarefasEntity, ListaTarefaUpdateCommand>().ReverseMap();

            CreateMap<ListaTarefaCreateCommand, ListaTarefasEntity>();

            CreateMap<ListaTarefaDto, ListaTarefaUpdateCommand>().ReverseMap();


            CreateMap<ListaTarefaUpdateCommand, ListaTarefasEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id));


            CreateMap<ListaTarefaUpdateCommand, ListaTarefasEntity>();

            CreateMap<ListaUpdateCommand, ListaEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdLista))
                .ForMember(dest => dest.ListaTarefas, opt => opt.MapFrom(src => src.ListaTarefas));


            CreateMap<ListaTarefaDto, ListaTarefasEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id)); ;




        }
    }
}

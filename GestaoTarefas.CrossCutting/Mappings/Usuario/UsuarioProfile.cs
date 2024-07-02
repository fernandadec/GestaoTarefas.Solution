using AutoMapper;
using GestaoTarefas.Domain.Dtos.Comum.Usuario;
using GestaoTarefas.Domain.Entities.Comum;


namespace GestaoTarefas.CrossCutting.Mappings.Usuario
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioEntity, UsuarioDto>().ReverseMap();
        }
    }
}

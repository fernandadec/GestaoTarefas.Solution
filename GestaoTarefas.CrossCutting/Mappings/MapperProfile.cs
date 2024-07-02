using AutoMapper;
using GestaoTarefas.CrossCutting.Mappings.Lista;
using GestaoTarefas.CrossCutting.Mappings.Usuario;

namespace GestaoTarefas.CrossCutting.Mappings
{
    public static class MapperProfile
    {
        public static MapperConfiguration Configure()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ListaProfile());
                cfg.AddProfile(new UsuarioProfile());
            });

            return config;
        }
    }
}
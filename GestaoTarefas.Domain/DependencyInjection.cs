using FluentValidation;
using MediatR;
using GestaoTarefas.Domain.Services.CryptographyService;
using GestaoTarefas.Domain.Services.Interfaces.CryptographyService;
using GestaoTarefas.Domain.Services.Interfaces.TokenService;
using GestaoTarefas.Domain.Services.TokenService;
using GestaoTarefas.Domain.RequestBehavior;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using GestaoTarefas.Domain.Query.Lista;
using GestaoTarefas.Domain.Dtos.Comum.Lista;
using GestaoTarefas.Domain.QueryHandler.Lista;

namespace GestaoTarefas.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ICryptographyService, CryptographyService>();

            services.AddScoped<ITokenService, TokenService>();
            
            services.AddMediatR(typeof(ListaCompleteQuery).Assembly);



            return services;
        }

        public static IServiceCollection AddFluentValidationConfiguration(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
        public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestBehaviorValidation<,>));

            return services;
        }
    }
}
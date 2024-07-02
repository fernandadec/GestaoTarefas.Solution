using GestaoTarefas.Data.Context;
using GestaoTarefas.Data.DataModule;
using GestaoTarefas.Domain.Interfaces.DataModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoTarefas.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApsContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("SQLServer"),
                    b => b.MigrationsAssembly(typeof(ApsContext).Assembly.FullName)));

            services.AddScoped<IDataModule, DataModule<ApsContext>>();
            services.AddScoped<IDataModuleDBAps, DataModuleDBAps>();

            return services;
        }
    }
}
namespace GestaoTarefas.Api.Configuration
{
    public static class CorsConfig
    {
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            return services;
        }

        public static IApplicationBuilder UseCorsConfiguration(this IApplicationBuilder app)
        {
            app.UseCors();
            return app;
        }
    }
}
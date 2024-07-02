using AutoMapper;
using GestaoTarefas.Domain.Dtos.AppSettings;
using MediatR;
using System.Reflection;
using GestaoTarefas.Domain;
using GestaoTarefas.Data;
using GestaoTarefas.Api.Configuration;
using GestaoTarefas.CrossCutting.Mappings;
using GestaoTarefas.Api.ErrorHandling;
using GestaoTarefas.Data.Context;
using Microsoft.EntityFrameworkCore;
using GestaoTarefas.Domain.Dtos.Comum.Lista;
using GestaoTarefas.Domain.Query.Lista;
using GestaoTarefas.Domain.QueryHandler.Lista;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddIdentityInfrastructure(builder.Configuration);

builder.Services.AddCorsConfiguration();

builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddJwtConfiguration(builder.Configuration, builder.Environment);

builder.Services.AddSwaggerConfiguration();

builder.Services.AddFluentValidationConfiguration();

builder.Services.AddAutoMapperConfiguration();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(typeof(ListaCompleteQueryHandler).Assembly);

// Registrando outros serviços necessários, se houver
builder.Services.AddScoped<IRequestHandler<ListaCompleteQuery, List<ListaDtoComplete>>, ListaCompleteQueryHandler>();



builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
});


builder.Services.AddMediatRConfiguration();

//services.AddAutoMapperConfiguration();
var mapperConfig = MapperProfile.Configure();

//Configurando Mapper.
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseCors();

app.UseRouting();

app.UseMiddleware(typeof(ErrorHandlingMiddleware));


app.UseJwtConfiguration();

app.UseAuthentication(); // Certifique-se de adicionar isso se estiver usando autenticação
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.UseJwtConfiguration();


if (builder.Configuration.GetValue<bool>("Migrations:Apply"))
{
    using (var service = app.Services
        .GetRequiredService<IServiceScopeFactory>()
        .CreateScope())
    {
        using (var context = service.ServiceProvider.GetService<ApsContext>())
        {
            context.Database.Migrate();
        }
    }
}


app.Run();

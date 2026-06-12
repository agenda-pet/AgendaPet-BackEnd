using AgendaPetAPI.Applications.Autentification;
using AgendaPetAPI.Applications.Service;
using AgendaPetAPI.Contexts;
using AgendaPetAPI.Interfaces;
using AgendaPetAPI.Repositories;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

// pegando a connection string
string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

// Conex�o com banco
builder.Services.AddDbContext<AgendaPetDbContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Value: Bearer TokenJWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Comportamento Pet
builder.Services.AddScoped<IComportamentoPetRepository, ComportamentoPetRepository>();
builder.Services.AddScoped<ComportamentoPetervice>();

// Porte Pet
builder.Services.AddScoped<IPortePetRepository, PortePetRepository>();
builder.Services.AddScoped<PortePetervice>();

//Raca Pet 
builder.Services.AddScoped<IRacaPetRepository, RacaPetRepository>();
builder.Services.AddScoped<RacaPetervice>();

//Status agendamento
builder.Services.AddScoped<IStatusAgendamentoRepository, StatusAgendamentoRepository>();
builder.Services.AddScoped<StatusAgendamentoService>();

//Servico
builder.Services.AddScoped<IServicoRepository, ServicoRepository>();
builder.Services.AddScoped<ServicoService>();

//Tipo animal
builder.Services.AddScoped<ITipoAnimalRepository, TipoAnimalRepository>();
builder.Services.AddScoped<TipoAnimalService>();

//Tipo usuario
builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();
builder.Services.AddScoped<TipoUsuarioService>();

//Log agendamento
builder.Services.AddScoped<ILogAgendamentoRepository, LogAgendamentoRepository>();
builder.Services.AddScoped<LogAgendamentoService>();

//Usuario 
builder.Services.AddScoped<IUsuarioRepository,  UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();

//Pet 
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<PetService>();

//JWT
builder.Services.AddScoped<GeradorTokenJWT>();
builder.Services.AddScoped<AutenticacaoService>();

var app = builder.Build();

builder.Services.AddAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

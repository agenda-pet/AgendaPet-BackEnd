using AgendaPetAPI.Aplications.Service;
using AgendaPetAPI.Applications.Service;
using AgendaPetAPI.Contexts;
using AgendaPetAPI.Domains;
using AgendaPetAPI.Interfaces;
using AgendaPetAPI.Repositories;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Comportamento Pet
builder.Services.AddScoped<IComportamentoPetRepository, ComportamentoPetRepository>();
builder.Services.AddScoped<ComportamentoPetService>();

// Porte Pet
builder.Services.AddScoped<IPortePetRepository, PortePetRepository>();
builder.Services.AddScoped<PortePetService>();

//Raca Pet 
builder.Services.AddScoped<IRacaPetRepository, RacaPetRepository>();
builder.Services.AddScoped<RacaPetService>();

//Status agendamento
builder.Services.AddScoped<IStatusAgendamentoRepository, StatusAgendamentoRepository>();
builder.Services.AddScoped<StatusAgendamentoService>();

//Status usuario
builder.Services.AddScoped<IStatusUsuarioRepository, StatusUsuarioRepository>();
builder.Services.AddScoped<StatusUsuarioService>();

//Tipo animal
builder.Services.AddScoped<ITipoAnimalRepository, TipoAnimalRepository>();
builder.Services.AddScoped<TipoAnimalService>();

//Tipo usuario
builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();
builder.Services.AddScoped<TipoUsuarioService>();

//Log agendamento
builder.Services.AddScoped<ILogAgendamentoRepository, LogAgendamentoRepository>();
builder.Services.AddScoped<LogAgendamentoService>();

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

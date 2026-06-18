using AgendaPetAPI.Applications.Autentification;
using AgendaPetAPI.Applications.Service;
using AgendaPetAPI.Contexts;
using AgendaPetAPI.Domains;
using AgendaPetAPI.Interfaces;
using AgendaPetAPI.Repositories;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();

//Pet 
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<PetService>();

//Agendamento
builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
builder.Services.AddScoped<AgendamentoService>();

//JWT
builder.Services.AddScoped<GeradorTokenJWT>();
builder.Services.AddScoped<AutenticacaoService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

    // Adiciona o suporte para autenticação usando JWT.
    .AddJwtBearer(options =>
    {
        // Lê a chave secreta definida no appsettings.json.
        var chave = Environment.GetEnvironmentVariable("JWT_KEY");
        //var chave = builder.Configuration["Jwt:Key"]!;

        // Quem emitiu o token.
        var issuer = builder.Configuration["Jwt:Issuer"]!;

        // Para quem o token foi criado.
        var audience = builder.Configuration["Jwt:Audience"]!;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Verifica se o emissor do token é válido.
            ValidateIssuer = true,

            // Verifica se o destinatário do token é válido.
            ValidateAudience = true,

            // Verifica se o token ainda está válido.
            ValidateLifetime = true,

            // Verifica se a assinatura do token é válida.
            ValidateIssuerSigningKey = true,

            // Define qual emissor é considerado válido.
            ValidIssuer = issuer,

            // Define qual audience é considerado válido.
            ValidAudience = audience,

            // Define qual chave será usada para validar a assinatura do token.
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(chave)
            ),

            // o token geralmente tem 5 minutos de tolerancia, aqui colocamos para remover essa tolerancia
            // remove tolerância extra no vencimento do token
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
using fiap_cloud_games_api.Services;
using fiap_cloud_games.Infrastructure.Repositories;
using fiap_cloud_games.Infrastructure.Settings;
using fiap_cloud_games.Domain.Interfaces;
using fiap_cloud_games_api.AutoMapper;
using AutoMapper;
using fiap_cloud_games.Services;
using fiap_cloud_games.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// --- Adicionar serviços ao container ---

// Adicionar Controllers
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar Settings
builder.Services.AddSingleton<MongoDbContext>();

// --- Injeção de Dependência para Repositórios (Scoped) ---

// Registrar IUsuarioRepository e sua implementação UsuarioRepository
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Registrar IJogoRepository e sua implementação JogoRepository
builder.Services.AddScoped<IJogoRepository, JogoRepository>();

// --- Injeção de Dependência para Serviços (Scoped) ---

// Registrar IUsuarioService e sua implementação UsuarioService
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// Registrar IAuthService e sua implementação AuthService
builder.Services.AddScoped<IAuthService, AuthService>();

// Registrar IJogoService e sua implementação JogoService
builder.Services.AddScoped<IJogoService, JogoService>();

// --- Adicionar AutoMapper ---

// Registrar AutoMapper com o perfil de mapeamento
builder.Services.AddAutoMapper(typeof(MappingProfile));

// --- Criar e configurar o aplicativo ---

var app = builder.Build();

// Configurar o pipeline de requisição HTTP

if (app.Environment.IsDevelopment())
{
    // Ativar Swagger se estiver em ambiente de desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar o middleware de autorização
app.UseAuthorization();

// Mapear Controllers
app.MapControllers();

// Executar o aplicativo
app.Run();

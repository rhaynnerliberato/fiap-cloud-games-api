using fiap_cloud_games_api.Services;
using fiap_cloud_games.Infrastructure.Repositories;
using fiap_cloud_games.Infrastructure.Settings;
using fiap_cloud_games.Domain.Interfaces;
using fiap_cloud_games_api.AutoMapper;
using fiap_cloud_games.Services;
using fiap_cloud_games.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

var builder = WebApplication.CreateBuilder(args);

// --- Registra o serializer para Guid com representação padrão "Standard" ---
// Versão 3.4.0 do MongoDB.Driver não possui GuidRepresentationMode nem IsSerializerRegistered
BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.GuidRepresentation.Standard));

// --- Adicionar serviços ao container ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- Configurar MongoDB Settings ---
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection(nameof(MongoDbSettings)));

// Configurar o MongoDbContext corretamente
builder.Services.AddSingleton<MongoDbContext>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new MongoDbContext(configuration);
});

// --- Configurar autenticação JWT ---
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero // Remove tolerância de tempo para expiração
    };
});

// --- Injeção de Dependência para Repositórios (Scoped) ---
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IJogoRepository, JogoRepository>();

// --- Injeção de Dependência para Serviços (Scoped) ---
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJogoService, JogoService>();

// --- AutoMapper ---
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// --- Configuração do pipeline HTTP ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

using fiap_cloud_games.Domain.Enums;
using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games.Infrastructure.Context;
using MongoDB.Driver;

namespace fiap_cloud_games.Infrastructure.Migrations
{
    public static class DatabaseInitializer
    {
        public static void Seed(MongoDbContext context)
        {
            var usuarios = context.Usuarios;
            var jogos = context.Jogos;

            // Criação de índice (e-mail único)
            var emailIndex = new CreateIndexModel<Usuario>(
                Builders<Usuario>.IndexKeys.Ascending(u => u.Email),
                new CreateIndexOptions { Unique = true }
            );
            usuarios.Indexes.CreateOne(emailIndex);


            // Seed inicial de usuario administrador
            if (!usuarios.Find(_ => true).Any())
            {
                usuarios.InsertOne(new Usuario
                {
                    Nome = "Admin Default",
                    Email = "admin@admin.com",
                    Senha = "AaDd!@#123456",
                    Perfil = PerfilUsuario.Administrador
                });
            }

            // Seed inicial de jogo
            if (!jogos.Find(_ => true).Any())
            {
                jogos.InsertOne(new Jogo
                {
                    Nome = "God of War",
                    Plataforma = "PS5",
                    Genero = "Ação",
                    Descricao = "Jogo de aventura com Kratos",
                    Valor = 299
                });
            }
        }
    }
}

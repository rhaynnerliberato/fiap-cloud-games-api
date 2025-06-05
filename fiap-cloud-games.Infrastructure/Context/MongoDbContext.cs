using fiap_cloud_games.Domain.Entities;
using MongoDB.Driver;

namespace fiap_cloud_games.Infrastructure.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("MongoDbSettings").GetValue<string>("ConnectionString");
            var databaseName = configuration.GetSection("MongoDbSettings").GetValue<string>("Database");

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Jogo> Jogos => _database.GetCollection<Jogo>("Jogos");
        public IMongoCollection<Usuario> Usuarios => _database.GetCollection<Usuario>("Usuarios");
    }
}

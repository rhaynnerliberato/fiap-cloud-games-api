using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games.Domain.Interfaces.Repositories;
using fiap_cloud_games.Infrastructure.Context;
using MongoDB.Bson;
using MongoDB.Driver;

namespace fiap_cloud_games.Infrastructure.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly IMongoCollection<Jogo> _jogos;

        public JogoRepository(MongoDbContext context)
        {
            _jogos = context.Jogos;
        }

        public async Task<IEnumerable<Jogo>> ListarAsync()
        {
            return await _jogos.Find(_ => true).ToListAsync();
        }

        public async Task<Jogo> ObterPorIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return null;

            return await _jogos.Find(j => j.Id == objectId).FirstOrDefaultAsync();
        }

        public async Task<Jogo> CadastrarAsync(Jogo jogo)
        {
            await _jogos.InsertOneAsync(jogo);
            return jogo;
        }

        public async Task AtualizarAsync(Jogo jogo)
        {
            await _jogos.ReplaceOneAsync(j => j.Id == jogo.Id, jogo);
        }

        public async Task DeletarAsync(Jogo jogo)
        {
            await _jogos.DeleteOneAsync(j => j.Id == jogo.Id);
        }
    }
}

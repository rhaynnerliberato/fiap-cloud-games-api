using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games.Domain.Interfaces;
using fiap_cloud_games.Infrastructure.Context;
using MongoDB.Bson;
using MongoDB.Driver;

namespace fiap_cloud_games.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IMongoCollection<Usuario> _usuarios;

        public UsuarioRepository(MongoDbContext context)
        {
            _usuarios = context.Usuarios;
        }

        public async Task CadastrarAsync(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            await _usuarios.InsertOneAsync(usuario);
        }

        public async Task<Usuario?> ObterPorEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            return await _usuarios.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<Usuario?> ObterPorIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return null;

            return await _usuarios.Find(u => u.Id == objectId).FirstOrDefaultAsync();
        }

        public async Task<List<Usuario>> ObterTodosAsync()
        {
            return await _usuarios.Find(_ => true).ToListAsync();
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            await _usuarios.ReplaceOneAsync(u => u.Id == usuario.Id, usuario);
        }

        public async Task DeletarAsync(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
                throw new ArgumentException("Id inválido", nameof(id));

            await _usuarios.DeleteOneAsync(u => u.Id == objectId);
        }
    }
}

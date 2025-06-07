using fiap_cloud_games.Domain.Entities;

namespace fiap_cloud_games.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task CadastrarAsync(Usuario usuario);
        Task<Usuario?> ObterPorEmailAsync(string email);
        Task<List<Usuario>> ObterTodosAsync();
        Task<Usuario?> ObterPorIdAsync(string id);
        Task AtualizarAsync(Usuario usuario);
        Task DeletarAsync(string id);
    }
}

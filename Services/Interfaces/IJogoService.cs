using fiap_cloud_games.Domain.Entities;

namespace fiap_cloud_games.Domain.Interfaces
{
    public interface IJogoService
    {
        Task<List<Jogo>> ListarAsync();
        Task<Jogo> ObterPorIdAsync(string id);
        Task<Jogo> CadastrarAsync(Jogo jogo);
        Task AtualizarAsync(Jogo jogo);
        Task DeletarAsync(Jogo jogo);
        Task<bool> AplicarPromocaoAsync(string id, decimal percentual);

    }
}

using fiap_cloud_games.API.DTOs.Responses;
using fiap_cloud_games.Domain.Entities;

namespace fiap_cloud_games.Application.Services.Interfaces
{
    public interface IJogoService
    {
        Task<List<JogoResponse>> ListarAsync();
        Task<Jogo> ObterPorIdAsync(string id);
        Task<JogoResponse> CadastrarAsync(Jogo jogo);
        Task AtualizarAsync(Jogo jogo);
        Task DeletarAsync(Jogo jogo);
        Task<bool> AplicarPromocaoAsync(string id, decimal percentual);
    }
}

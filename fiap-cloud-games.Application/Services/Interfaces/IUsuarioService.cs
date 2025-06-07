using fiap_cloud_games.API.DTOs.Responses;
using fiap_cloud_games.Domain.Entities;

namespace fiap_cloud_games.Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResponse> CadastrarAsync(Usuario usuarioCreate);
        Task<IEnumerable<UsuarioResponse>> ObterTodosAsync();
        Task<UsuarioResponse?> ObterPorIdAsync(string id);
        Task<UsuarioResponse?> AtualizarAsync(string id, Usuario usuarioUpdate);
        Task<bool> DeletarAsync(string id);
    }
}
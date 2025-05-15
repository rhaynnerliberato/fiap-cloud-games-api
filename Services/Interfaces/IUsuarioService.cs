using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games_api.Requests;
using fiap_cloud_games_api.LoginRequests;
using fiap_cloud_games_api.Responses;

namespace fiap_cloud_games.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResponse> CadastrarAsync(UsuarioCreateRequest usuarioCreate);
        Task<Usuario?> AutenticarAsync(LoginRequest login);
    }
}

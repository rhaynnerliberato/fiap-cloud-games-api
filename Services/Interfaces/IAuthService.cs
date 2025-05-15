using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games_api.Models.Requests;

namespace fiap_cloud_games.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<string> Authenticate(LoginRequest request);
        string GerarToken(Usuario usuario);
        bool ValidarToken(string token);
    }
}

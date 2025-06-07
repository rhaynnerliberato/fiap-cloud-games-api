using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games.Domain.ValueObjects;

namespace fiap_cloud_games.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Authenticate(AuthCredentials credentials);
        string GerarToken(Usuario usuario);
        bool ValidarToken(string token);
    }
}

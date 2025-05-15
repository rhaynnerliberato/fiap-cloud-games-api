using fiap_cloud_games.Domain.Entities;

namespace fiap_cloud_games.Domain.Interfaces
{
    public interface IAuthService
    {
        string GerarToken(Usuario usuario);
        bool ValidarToken(string token);
    }
}

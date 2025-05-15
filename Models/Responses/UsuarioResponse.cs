using fiap_cloud_games.Domain.Enums;

namespace fiap_cloud_games_api.Models.Responses
{
    public class UsuarioResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public PerfilUsuario Perfil { get; set; }
    }
}

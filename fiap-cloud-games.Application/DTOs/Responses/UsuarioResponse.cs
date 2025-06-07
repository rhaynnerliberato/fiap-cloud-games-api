using MongoDB.Bson;

namespace fiap_cloud_games.API.DTOs.Responses
{
    public class UsuarioResponse
    {
        public ObjectId Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Perfil { get; set; }
    }
}

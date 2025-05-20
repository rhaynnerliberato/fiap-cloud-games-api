using MongoDB.Bson;

namespace fiap_cloud_games_api.Models.Requests
{
    public class JogoRequest
    {
        public ObjectId Id { get; set; }
        public required string Nome { get; set; }
        public required string Plataforma { get; set; }
        public required string Genero { get; set; }
        public string? Descricao { get; set; }
        public required string Valor { get; set; }
    }
}

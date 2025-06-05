using MongoDB.Bson;

namespace fiap_cloud_games_api.Models.Responses
{
    public class JogoResponse
    {
        public ObjectId Id { get; set; }
        public string Nome { get; set; }
        public string Plataforma { get; set; }
        public string Genero { get; set; }
        public string Descricao { get; set; }
        public string Valor { get; set; }
    }
}

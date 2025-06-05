using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fiap_cloud_games.Domain.Entities
{
    public class Jogo
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Plataforma { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; } = 0;
    }
}

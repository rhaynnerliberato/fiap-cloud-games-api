using MongoDB.Bson;
using System;

namespace fiap_cloud_games.API.DTOs.Responses
{
    public class JogoResponse
    {
        public ObjectId Id { get; set; }
        public string Nome { get; set; }
        public string Plataforma { get; set; }
        public string Genero { get; set; }
        public string Descricao { get; set; }
        public Decimal Valor { get; set; }
    }
}

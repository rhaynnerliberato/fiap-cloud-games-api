namespace fiap_cloud_games_api.Models.Requests
{
    public class JogoRequest
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Plataforma { get; set; }
        public string Genero { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}

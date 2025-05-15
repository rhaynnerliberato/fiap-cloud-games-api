namespace fiap_cloud_games_api.Requests
{
    public class JogoCreateRequest
    {
        public string Nome { get; set; }
        public string Plataforma { get; set; }
        public string Genero { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}

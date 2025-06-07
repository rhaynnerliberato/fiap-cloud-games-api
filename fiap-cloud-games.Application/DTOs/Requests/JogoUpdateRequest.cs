namespace fiap_cloud_games.Application.DTOs.Requests
{
    public class JogoUpdateRequest
    {
        public string? Nome { get; set; }
        public string? Plataforma { get; set; }
        public string? Genero { get; set; }
        public string? Descricao { get; set; }
        public decimal? Valor { get; set; }
    }
}

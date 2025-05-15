namespace fiap_cloud_games_api.Requests
{
    public class UsuarioCreateRequest
    {
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
    }
}

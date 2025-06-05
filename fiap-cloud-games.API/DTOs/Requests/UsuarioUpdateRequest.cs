namespace fiap_cloud_games_api.Models.Requests
{
    public class UsuarioUpdateRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Senha { get; set; } // Opcional, caso o usuário não queira trocar
    }
}

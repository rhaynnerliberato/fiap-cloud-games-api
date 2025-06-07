namespace fiap_cloud_games.Domain.ValueObjects
{
    public class AuthCredentials
    {
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }
}

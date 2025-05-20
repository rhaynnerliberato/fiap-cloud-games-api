using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games.Domain.Interfaces;
using fiap_cloud_games_api.Models.Requests;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fiap_cloud_games_api.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _jwtSecret;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<AuthService> _logger;
        private IConfigurationRoot configMock;
        private IUsuarioRepository @object;

        public AuthService(IConfiguration configuration, IUsuarioRepository usuarioRepository, ILogger<AuthService> logger)
        {
            _jwtSecret = configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key não configurado.");
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        public async Task<string> Authenticate(LoginRequest request)
        {
            _logger.LogInformation("Autenticação iniciada para o e-mail: {Email}", request.Email);

            var usuario = await _usuarioRepository.ObterPorEmailAsync(request.Email);

            if (usuario != null)
            {
                if (usuario.Senha == request.Senha)
                {
                    _logger.LogInformation("Usuário autenticado com sucesso. ID: {Id}, Perfil: {Perfil}", usuario.Id, usuario.Perfil);
                    return GerarToken(usuario);
                }

                _logger.LogWarning("Senha inválida fornecida para o e-mail: {Email}", request.Email);
            }
            else
            {
                _logger.LogWarning("Usuário não encontrado para o e-mail: {Email}", request.Email);
            }

            throw new UnauthorizedAccessException("Email ou senha inválidos.");
        }

        public string GerarToken(Usuario usuario)
        {
            _logger.LogInformation("Gerando token JWT para o usuário. ID: {Id}, Email: {Email}", usuario.Id, usuario.Email);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            string perfil = usuario.Perfil?.ToString() ?? "Usuario";

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Email),
                new Claim(ClaimTypes.Role, perfil)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            _logger.LogInformation("Token JWT gerado com sucesso para o usuário ID: {Id}", usuario.Id);
            return tokenString;
        }

        public bool ValidarToken(string token)
        {
            _logger.LogInformation("Iniciando validação do token JWT.");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                _logger.LogInformation("Token JWT validado com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Falha na validação do token JWT.");
                return false;
            }
        }
    }
}

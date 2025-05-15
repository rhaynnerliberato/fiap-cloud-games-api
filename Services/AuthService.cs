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

        public AuthService(IConfiguration configuration, IUsuarioRepository usuarioRepository)
        {
            _jwtSecret = configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key não configurado.");
            _usuarioRepository = usuarioRepository;
        }

        public async Task<string> Authenticate(LoginRequest request)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(request.Email);

            if (usuario != null)
            {
                // ⚠️ Recomendado: usar hash de senha (ex: BCrypt)
                if (usuario.Senha == request.Senha)
                {
                    return GerarToken(usuario);
                }
            }

            // Retorno explícito para facilitar depuração
            throw new UnauthorizedAccessException("Email ou senha inválidos.");
        }

        public string GerarToken(Usuario usuario)
        {
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
            return tokenHandler.WriteToken(token);
        }

        public bool ValidarToken(string token)
        {
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

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games.Domain.Interfaces;
using fiap_cloud_games_api.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fiap_cloud_games_api.Services
{
    public class AuthService : IAuthService
    {
        public string Authenticate(LoginDTO loginDto)
        {
            if (loginDto.Email == "admin@fiap.com" && loginDto.Senha == "Admin@123")
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("sua-chave-jwt-secreta-aqui");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, loginDto.Email),
                        new Claim(ClaimTypes.Role, "Administrador")
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            return string.Empty;
        }

        public string GerarToken(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public bool ValidarToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}

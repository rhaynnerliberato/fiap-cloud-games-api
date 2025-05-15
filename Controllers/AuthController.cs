using fiap_cloud_games_api.LoginRequests;
using fiap_cloud_games_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace fiap_cloud_games_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var token = _authService.Authenticate(request);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { Message = "Credenciais inválidas. Verifique seu email e senha." });
            }

            return Ok(new { Token = token });
        }
    }
}

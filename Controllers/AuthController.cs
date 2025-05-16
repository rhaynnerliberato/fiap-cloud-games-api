using fiap_cloud_games.Domain.Interfaces;
using fiap_cloud_games_api.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fiap_cloud_games_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Realiza o login e retorna o token JWT.
        /// </summary>
        /// <param name="request">e-mail e senha.</param>
        /// <returns>Token JWT.</returns>
        [AllowAnonymous] // Permite que usuários não autenticados acessem este endpoint
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = await _authService.Authenticate(request);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Erro interno ao processar a autenticação." });
            }
        }
    }
}

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
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
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
            _logger.LogInformation("Tentativa de login para o e-mail: {Email}", request.Email);

            try
            {
                var token = await _authService.Authenticate(request);
                _logger.LogInformation("Login bem-sucedido para o e-mail: {Email}", request.Email);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning("Falha de autenticação para o e-mail: {Email} - {Message}", request.Email, ex.Message);
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao processar login para o e-mail: {Email}", request.Email);
                return StatusCode(500, new { Message = "Erro interno ao processar a autenticação." });
            }
        }

        // Endpoint criado para testar o middleware de tratamento global de erros
        [HttpGet("erro")]
        public IActionResult TestarErro()
        {
            throw new Exception("Erro de teste lançado propositalmente.");
        }
    }
}

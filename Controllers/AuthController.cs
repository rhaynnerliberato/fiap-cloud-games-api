using fiap_cloud_games_api.DTOs;
using fiap_cloud_games_api.Services;
using Microsoft.AspNetCore.Mvc;
using fiap_cloud_games.Domain.Entities;

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
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            var token = _authService.Authenticate(loginDto);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            return Ok(new { Token = token });
        }


    }
}

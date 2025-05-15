using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using fiap_cloud_games.Domain.Interfaces;
using fiap_cloud_games_api.Responses;
using fiap_cloud_games_api.Requests;

namespace fiap_cloud_games_api.Controllers
{
    [ApiController]
    [Route("api/cadastrar/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("cadastrar")]
        public async Task<ActionResult<UsuarioResponse>> Cadastrar([FromBody] UsuarioCreateRequest request)
        {
            if (!Regex.IsMatch(request.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return BadRequest("E-mail inválido.");

            if (!Regex.IsMatch(request.Senha, @"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[\W_]).{8,}$"))
                return BadRequest("Senha inválida. A senha deve ter no mínimo 8 caracteres, incluindo letras, números e um caractere especial.");

            var usuarioResponse = await _usuarioService.CadastrarAsync(request);
            return Ok(usuarioResponse);
        }

    }
}

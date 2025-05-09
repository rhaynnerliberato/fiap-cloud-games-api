using Microsoft.AspNetCore.Mvc;
using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games_api.DTOs;
using fiap_cloud_games_api.Services;


namespace fiap_cloud_games_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        public UsuariosController(UsuarioService usuarioService)
        {
           _usuarioService = usuarioService;
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] UsuarioCreateDTO dto)
        {
            var usuario = _usuarioService.Cadastrar(dto);
            return CreatedAtAction(nameof(Cadastrar), new { id = usuario.Id}, usuario);
        }
    }
}

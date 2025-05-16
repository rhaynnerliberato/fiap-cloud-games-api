using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using fiap_cloud_games.Domain.Interfaces;
using fiap_cloud_games_api.Models.Requests;
using fiap_cloud_games_api.Models.Responses;
using Microsoft.AspNetCore.Authorization;

namespace fiap_cloud_games_api.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Cadastra um novo usuário.
        /// </summary>
        /// <param name="request">Dados do novo usuário.</param>
        /// <returns>Usuário cadastrado.</returns>
        [AllowAnonymous]
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

        /// <summary>
        /// Lista todos os usuários cadastrados.
        /// </summary>
        /// <returns>Lista de usuários.</returns>
        [Authorize(Roles = "Administrador")]
        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<UsuarioResponse>>> ListarTodos()
        {
            var usuarios = await _usuarioService.ObterTodosAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Busca um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>Usuário encontrado.</returns>
        [Authorize(Roles = "Administrador")]
        [HttpGet("buscar-usuario/{id}")]
        public async Task<ActionResult<UsuarioResponse>> ObterPorId(string id)
        {
            var usuario = await _usuarioService.ObterPorIdAsync(id);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        /// <summary>
        /// Atualiza os dados de um usuário.
        /// </summary>
        /// <param name="id">ID do usuário a ser atualizado.</param>
        /// <param name="request">Novos dados do usuário.</param>
        /// <returns>Usuário atualizado.</returns>
        [Authorize(Roles = "Administrador")]
        [HttpPut("alterar-usuario/{id}")]
        public async Task<ActionResult<UsuarioResponse>> Atualizar(string id, [FromBody] UsuarioUpdateRequest request)
        {
            var usuarioAtualizado = await _usuarioService.AtualizarAsync(id, request);
            if (usuarioAtualizado == null)
                return NotFound();

            return Ok(usuarioAtualizado);
        }

        /// <summary>
        /// Remove um usuário do sistema.
        /// </summary>
        /// <param name="id">ID do usuário a ser removido.</param>
        [Authorize(Roles = "Administrador")]
        [HttpDelete("deletar-usuario/{id}")]
        public async Task<IActionResult> Deletar(string id)
        {
            var deletado = await _usuarioService.DeletarAsync(id);
            if (!deletado)
                return NotFound();

            return NoContent();
        }
    }
}

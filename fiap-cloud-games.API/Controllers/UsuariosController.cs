using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using fiap_cloud_games.API.DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using fiap_cloud_games.Application.DTOs.Requests;
using AutoMapper;
using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games.Application.Services.Interfaces;

namespace fiap_cloud_games.API.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<UsuariosController> _logger;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioService usuarioService, ILogger<UsuariosController> logger, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Cadastra um novo usuário.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("cadastrar")]
        public async Task<ActionResult<UsuarioResponse>> Cadastrar([FromBody] UsuarioCreateRequest request)
        {
            _logger.LogInformation("Iniciando cadastro de novo usuário com email: {Email}", request.Email);

            if (!Regex.IsMatch(request.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                _logger.LogWarning("Email inválido fornecido: {Email}", request.Email);
                return BadRequest("E-mail inválido.");
            }

            if (!Regex.IsMatch(request.Senha, @"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[\W_]).{8,}$"))
            {
                _logger.LogWarning("Senha inválida fornecida para email: {Email}", request.Email);
                return BadRequest("Senha inválida. A senha deve ter no mínimo 8 caracteres, incluindo letras, números e um caractere especial.");
            }

            var usuario = _mapper.Map<Usuario>(request);
            var usuarioResponse = await _usuarioService.CadastrarAsync(usuario);

            _logger.LogInformation("Usuário cadastrado com sucesso. ID: {Id}", usuarioResponse.Id);
            return Ok(usuarioResponse);
        }

        /// <summary>
        /// Lista todos os usuários cadastrados.
        /// </summary>
        [Authorize(Roles = "Administrador")]
        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<UsuarioResponse>>> ListarTodos()
        {
            _logger.LogInformation("Listando todos os usuários");
            var usuarios = await _usuarioService.ObterTodosAsync();
            _logger.LogInformation("Total de usuários encontrados: {Count}", usuarios.Count());
            return Ok(usuarios);
        }

        /// <summary>
        /// Busca um usuário pelo ID.
        /// </summary>
        [Authorize(Roles = "Administrador")]
        [HttpGet("buscar-usuario/{id}")]
        public async Task<ActionResult<UsuarioResponse>> ObterPorId(string id)
        {
            _logger.LogInformation("Buscando usuário com ID: {Id}", id);
            var usuario = await _usuarioService.ObterPorIdAsync(id);
            if (usuario == null)
            {
                _logger.LogWarning("Usuário não encontrado com ID: {Id}", id);
                return NotFound();
            }

            _logger.LogInformation("Usuário encontrado com ID: {Id}", id);
            return Ok(usuario);
        }

        /// <summary>
        /// Atualiza os dados de um usuário.
        /// </summary>
        [Authorize(Roles = "Administrador")]
        [HttpPut("alterar-usuario/{id}")]
        public async Task<ActionResult<UsuarioResponse>> Atualizar(string id, [FromBody] UsuarioUpdateRequest request)
        {
            _logger.LogInformation("Atualizando usuário com ID: {Id}", id);
            var usuario = _mapper.Map<Usuario>(request);
            var usuarioAtualizado = await _usuarioService.AtualizarAsync(id, usuario);

            if (usuarioAtualizado == null)
            {
                _logger.LogWarning("Tentativa de atualizar usuário inexistente com ID: {Id}", id);
                return NotFound();
            }

            _logger.LogInformation("Usuário atualizado com sucesso. ID: {Id}", id);
            return Ok(usuarioAtualizado);
        }

        /// <summary>
        /// Remove um usuário do sistema.
        /// </summary>
        [Authorize(Roles = "Administrador")]
        [HttpDelete("deletar-usuario/{id}")]
        public async Task<IActionResult> Deletar(string id)
        {
            _logger.LogInformation("Tentando deletar usuário com ID: {Id}", id);
            var deletado = await _usuarioService.DeletarAsync(id);
            if (!deletado)
            {
                _logger.LogWarning("Usuário não encontrado para deletar com ID: {Id}", id);
                return NotFound();
            }

            _logger.LogInformation("Usuário deletado com sucesso. ID: {Id}", id);
            return NoContent();
        }
    }
}

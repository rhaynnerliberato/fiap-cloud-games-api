using AutoMapper;
using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using fiap_cloud_games_api.Models.Requests;
using fiap_cloud_games_api.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace fiap_cloud_games_api.Controllers
{
    [Route("api/jogos")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private readonly IJogoService _jogoService;
        private readonly IMapper _mapper;
        private readonly ILogger<JogoController> _logger;

        public JogoController(IJogoService jogoService, IMapper mapper, ILogger<JogoController> logger)
        {
            _jogoService = jogoService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Cadastra um novo jogo.
        /// </summary>
        /// <param name="request">Dados do novo jogo.</param>
        /// <returns>Jogo cadastrado.</returns>
        [Authorize(Roles = "Administrador")]
        [HttpPost("cadastrar")]
        public async Task<ActionResult<JogoResponse>> Cadastrar(JogoCreateRequest request)
        {
            _logger.LogInformation("Iniciando cadastro de novo jogo.");
            var jogo = _mapper.Map<Jogo>(request);
            var jogoCadastrado = await _jogoService.CadastrarAsync(jogo);
            _logger.LogInformation("Jogo cadastrado com sucesso. ID: {JogoId}", jogoCadastrado.Id);

            var jogoDTO = _mapper.Map<JogoResponse>(jogoCadastrado);
            return CreatedAtAction(nameof(ObterPorId), new { id = jogoDTO.Id }, jogoDTO);
        }

        /// <summary>
        /// Lista todos os jogos disponíveis.
        /// </summary>
        /// <returns>Lista de jogos.</returns>
        [Authorize]
        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<JogoResponse>>> Listar()
        {
            _logger.LogInformation("Listando todos os jogos.");
            var jogos = await _jogoService.ListarAsync();
            var jogosDTO = _mapper.Map<IEnumerable<JogoResponse>>(jogos);
            return Ok(jogosDTO);
        }

        /// <summary>
        /// Busca um jogo por ID.
        /// </summary>
        /// <param name="id">ID do jogo.</param>
        /// <returns>Dados do jogo encontrado.</returns>
        [Authorize]
        [HttpGet("buscar-jogo/{id}")]
        public async Task<ActionResult<JogoResponse>> ObterPorId(string id)
        {
            _logger.LogInformation("Buscando jogo por ID: {JogoId}", id);
            var jogo = await _jogoService.ObterPorIdAsync(id);
            if (jogo == null)
            {
                _logger.LogWarning("Jogo com ID {JogoId} não encontrado.", id);
                return NotFound();
            }

            var jogoDTO = _mapper.Map<JogoResponse>(jogo);
            return Ok(jogoDTO);
        }

        /// <summary>
        /// Atualiza os dados de um jogo.
        /// </summary>
        /// <param name="id">ID do jogo a ser atualizado.</param>
        /// <param name="request">Novos dados do jogo.</param>
        [Authorize(Roles = "Administrador")]
        [HttpPut("alterar-jogo/{id}")]
        public async Task<IActionResult> Atualizar(string id, JogoUpdateRequest request)
        {
            _logger.LogInformation("Atualizando jogo com ID: {JogoId}", id);
            var jogoExistente = await _jogoService.ObterPorIdAsync(id);
            if (jogoExistente == null)
            {
                _logger.LogWarning("Jogo com ID {JogoId} não encontrado para atualização.", id);
                return NotFound();
            }

            _mapper.Map(request, jogoExistente);
            await _jogoService.AtualizarAsync(jogoExistente);

            _logger.LogInformation("Jogo com ID {JogoId} atualizado com sucesso.", id);
            return NoContent();
        }

        /// <summary>
        /// Remove um jogo do sistema.
        /// </summary>
        /// <param name="id">ID do jogo a ser removido.</param>
        [Authorize(Roles = "Administrador")]
        [HttpDelete("deletar-jogo/{id}")]
        public async Task<IActionResult> Deletar(string id)
        {
            _logger.LogInformation("Tentando deletar jogo com ID: {JogoId}", id);
            var jogo = await _jogoService.ObterPorIdAsync(id);
            if (jogo == null)
            {
                _logger.LogWarning("Jogo com ID {JogoId} não encontrado para deleção.", id);
                return NotFound();
            }

            await _jogoService.DeletarAsync(jogo);
            _logger.LogInformation("Jogo com ID {JogoId} deletado com sucesso.", id);
            return NoContent();
        }

        /// <summary>
        /// Lança valor promocional em um jogo existente.
        /// </summary>
        /// <param name="id">ID do jogo a receber valor promocional.</param>
        [HttpPost("{id}/promocao")]
        [Authorize] // Garante que o usuário esteja autenticado
        public async Task<IActionResult> AplicarPromocao(string id, [FromBody] AplicarPromocaoRequest request)
        {
            var email = User.Identity?.Name;

            if (User.FindFirst(ClaimTypes.Role)?.Value != "Administrador")
            {
                _logger.LogWarning("Usuário não autorizado tentou aplicar promoção. Email: {Email}", email);
                return Forbid();
            }

            _logger.LogInformation("Usuário {Email} solicitou aplicar {Percentual}% de desconto no jogo {Id}", email, request.Percentual, id);

            var sucesso = await _jogoService.AplicarPromocaoAsync(id, request.Percentual);
            if (!sucesso)
                return NotFound($"Jogo com ID {id} não encontrado.");

            return Ok($"Desconto de {request.Percentual}% aplicado com sucesso ao jogo {id}.");
        }
    }
}

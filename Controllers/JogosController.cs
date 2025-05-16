using AutoMapper;
using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using fiap_cloud_games_api.Models.Requests;
using fiap_cloud_games_api.Models.Responses;
using Microsoft.AspNetCore.Authorization;

namespace fiap_cloud_games_api.Controllers
{
    [Route("api/jogos")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private readonly IJogoService _jogoService;
        private readonly IMapper _mapper;

        public JogoController(IJogoService jogoService, IMapper mapper)
        {
            _jogoService = jogoService;
            _mapper = mapper;
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
            var jogo = _mapper.Map<Jogo>(request);
            var jogoCadastrado = await _jogoService.CadastrarAsync(jogo);

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
            var jogo = await _jogoService.ObterPorIdAsync(id);
            if (jogo == null)
                return NotFound();

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
            var jogoExistente = await _jogoService.ObterPorIdAsync(id);
            if (jogoExistente == null)
                return NotFound();

            _mapper.Map(request, jogoExistente);
            await _jogoService.AtualizarAsync(jogoExistente);

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
            var jogo = await _jogoService.ObterPorIdAsync(id);
            if (jogo == null)
                return NotFound();

            await _jogoService.DeletarAsync(jogo);
            return NoContent();
        }
    }
}

using AutoMapper;
using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using fiap_cloud_games_api.Models.Requests;
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

        [Authorize]
        [HttpGet("listar-jogos")]
        public async Task<ActionResult<IEnumerable<JogoRequest>>> Listar()
        {
            var jogos = await _jogoService.ListarAsync();
            var jogosDTO = _mapper.Map<IEnumerable<JogoRequest>>(jogos);
            return Ok(jogosDTO);
        }

        [Authorize]
        [HttpGet("buscar-jogo/{id}")]
        public async Task<ActionResult<JogoRequest>> ObterPorId(Guid id)
        {
            var jogo = await _jogoService.ObterPorIdAsync(id);
            if (jogo == null)
            {
                return NotFound();
            }
            var jogoDTO = _mapper.Map<JogoRequest>(jogo);
            return Ok(jogoDTO);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("cadastrar")]
        public async Task<ActionResult<JogoRequest>> Criar(JogoCreateRequest request)
        {
            var jogo = _mapper.Map<Jogo>(request);
            var jogoCadastrado = await _jogoService.CadastrarAsync(jogo);

            var jogoDTO = _mapper.Map<JogoRequest>(jogoCadastrado);
            return CreatedAtAction(nameof(ObterPorId), new { id = jogoDTO.Id }, jogoDTO);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("alterar-jogo/{id}")]
        public async Task<IActionResult> Atualizar(Guid id, JogoUpdateRequest request)
        {
            var jogoExistente = await _jogoService.ObterPorIdAsync(id);
            if (jogoExistente == null)
            {
                return NotFound();
            }

            _mapper.Map(request, jogoExistente);
            await _jogoService.AtualizarAsync(jogoExistente);

            return NoContent();
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("deletar-jogo/{id}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var jogo = await _jogoService.ObterPorIdAsync(id);
            if (jogo == null)
            {
                return NotFound();
            }

            await _jogoService.DeletarAsync(jogo);
            return NoContent();
        }
    }
}

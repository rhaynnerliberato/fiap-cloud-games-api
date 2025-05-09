using AutoMapper;
using fiap_cloud_games_api.DTOs;
using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using fiap_cloud_games_api.Services;

namespace fiap_cloud_games_api.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoDTO>>> Listar()
        {
            var jogos = await _jogoService.ListarAsync();
            var jogosDTO = _mapper.Map<IEnumerable<JogoDTO>>(jogos);
            return Ok(jogosDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JogoDTO>> ObterPorId(Guid id)
        {
            var jogo = await _jogoService.ObterPorIdAsync(id);
            if (jogo == null)
            {
                return NotFound();
            }
            var jogoDTO = _mapper.Map<JogoDTO>(jogo);
            return Ok(jogoDTO);
        }

        [HttpPost]
        public async Task<ActionResult<JogoDTO>> Criar(JogoCreateDTO jogoCreateDTO)
        {
            var jogo = _mapper.Map<Jogo>(jogoCreateDTO);
            var jogoCadastrado = await _jogoService.CadastrarAsync(jogo);

            var jogoDTO = _mapper.Map<JogoDTO>(jogoCadastrado);
            return CreatedAtAction(nameof(ObterPorId), new { id = jogoDTO.Id }, jogoDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, JogoUpdateDTO jogoUpdateDTO)
        {
            var jogoExistente = await _jogoService.ObterPorIdAsync(id);
            if (jogoExistente == null)
            {
                return NotFound();
            }

            _mapper.Map(jogoUpdateDTO, jogoExistente);
            await _jogoService.AtualizarAsync(jogoExistente);

            return NoContent();
        }

        [HttpDelete("{id}")]
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

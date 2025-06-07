using AutoMapper;
using fiap_cloud_games.API.DTOs.Responses;
using fiap_cloud_games.Application.Services.Interfaces;
using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games.Domain.Interfaces.Repositories;

namespace fiap_cloud_games.Application.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly ILogger<JogoService> _logger;
        private readonly IMapper _mapper;


        public JogoService(IJogoRepository jogoRepository, ILogger<JogoService> logger, IMapper mapper)
        {
            _jogoRepository = jogoRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<JogoResponse>> ListarAsync()
        {
            _logger.LogInformation("Listando todos os jogos.");
            var jogos = await _jogoRepository.ListarAsync();
            _logger.LogInformation("Total de jogos encontrados: {Quantidade}", jogos.Count());
            return _mapper.Map<List<JogoResponse>>(jogos);
        }

        public async Task<Jogo> ObterPorIdAsync(string id)
        {
            _logger.LogInformation("Buscando jogo com ID: {Id}", id);
            var jogo = await _jogoRepository.ObterPorIdAsync(id);

            if (jogo == null)
                _logger.LogWarning("Jogo com ID {Id} não encontrado.", id);
            else
                _logger.LogInformation("Jogo com ID {Id} encontrado: {Nome}", id, jogo.Nome);

            return jogo;
        }

        public async Task<JogoResponse> CadastrarAsync(Jogo jogo)
        {
            _logger.LogInformation("Cadastrando novo jogo: {Nome}", jogo.Nome);
            await _jogoRepository.CadastrarAsync(jogo);
            _logger.LogInformation("Jogo cadastrado com sucesso. ID: {Id}", jogo.Id);
            return _mapper.Map<JogoResponse>(jogo);
        }

        public async Task AtualizarAsync(Jogo jogo)
        {
            _logger.LogInformation("Atualizando jogo com ID: {Id}", jogo.Id);
            await _jogoRepository.AtualizarAsync(jogo);
            _logger.LogInformation("Jogo atualizado com sucesso. ID: {Id}", jogo.Id);
        }

        public async Task DeletarAsync(Jogo jogo)
        {
            _logger.LogInformation("Deletando jogo com ID: {Id}", jogo.Id);
            await _jogoRepository.DeletarAsync(jogo);
            _logger.LogInformation("Jogo deletado com sucesso. ID: {Id}", jogo.Id);
        }

        public async Task<bool> AplicarPromocaoAsync(string id, decimal percentual)
        {
            _logger.LogInformation("Aplicando promoção ao jogo {Id} com {Percentual}% de desconto", id, percentual);

            var jogo = await _jogoRepository.ObterPorIdAsync(id);
            if (jogo == null)
            {
                _logger.LogWarning("Jogo com ID {Id} não encontrado para aplicar desconto", id);
                return false;
            }

            var valorOriginal = jogo.Valor;
            jogo.Valor -= jogo.Valor * (percentual / 100);

            await _jogoRepository.AtualizarAsync(jogo);

            _logger.LogInformation("Desconto aplicado com sucesso ao jogo {Id}. Preço original: {Original}, Novo preço: {Novo}", id, valorOriginal, jogo.Valor);

            return true;
        }
    }
}

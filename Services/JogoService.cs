using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games.Domain.Interfaces;

namespace fiap_cloud_games.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task<List<Jogo>> ListarAsync()
        {
            var jogos = await _jogoRepository.ListarAsync();
            return jogos.ToList();
        }


        public async Task<Jogo> ObterPorIdAsync(Guid id)
        {
            return await _jogoRepository.ObterPorIdAsync(id);
        }

        public async Task<Jogo> CadastrarAsync(Jogo jogo)
        {
            await _jogoRepository.CadastrarAsync(jogo);
            return jogo;
        }

        public async Task AtualizarAsync(Jogo jogo)
        {
            await _jogoRepository.AtualizarAsync(jogo);
        }

        public async Task DeletarAsync(Jogo jogo)
        {
            await _jogoRepository.DeletarAsync(jogo);
        }
    }
}

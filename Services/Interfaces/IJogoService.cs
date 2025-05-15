using fiap_cloud_games.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fiap_cloud_games.Domain.Interfaces
{
    public interface IJogoService
    {
        Task<List<Jogo>> ListarAsync();
        Task<Jogo> ObterPorIdAsync(Guid id);
        Task<Jogo> CadastrarAsync(Jogo jogo);
        Task AtualizarAsync(Jogo jogo);
        Task DeletarAsync(Jogo jogo);
    }
}

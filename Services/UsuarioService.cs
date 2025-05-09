using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games.Domain.Interfaces;
using fiap_cloud_games.Infrastructure.Repositories;
using fiap_cloud_games_api.DTOs;

namespace fiap_cloud_games_api.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Usuario Cadastrar(UsuarioCreateDTO dto)
        {
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha
            };

            // Operação assíncrona, mas controladores atuais são síncronos. Solução rápida:
            _usuarioRepository.CadastrarAsync(usuario);

            return usuario;
        }

        public async Task<Usuario?> Autenticar(LoginDTO dto)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(dto.Email);
            return usuario;
        }

        public void CadastrarUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Usuario ObterUsuarioPorEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}

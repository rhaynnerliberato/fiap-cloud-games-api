using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games.Domain.Enums;
using fiap_cloud_games.Domain.Interfaces;
using AutoMapper;
using fiap_cloud_games_api.Models.Requests;
using fiap_cloud_games_api.Models.Responses;

namespace fiap_cloud_games_api.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<UsuarioResponse> CadastrarAsync(UsuarioCreateRequest request)
        {
            var usuario = _mapper.Map<Usuario>(request);

            usuario.Perfil = request.Email.EndsWith("@fiap.com", StringComparison.OrdinalIgnoreCase)
                ? PerfilUsuario.Administrador
                : PerfilUsuario.Jogador;

            await _usuarioRepository.CadastrarAsync(usuario);

            var response = _mapper.Map<UsuarioResponse>(usuario);
            return response;
        }

        public async Task<Usuario?> AutenticarAsync(LoginRequest dto)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(dto.Email);
            return usuario;
        }

        public async Task<IEnumerable<UsuarioResponse>> ObterTodosAsync()
        {
            var usuarios = await _usuarioRepository.ObterTodosAsync();
            return usuarios.Select(u => _mapper.Map<UsuarioResponse>(u));
        }

        public async Task<UsuarioResponse?> ObterPorIdAsync(string id)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);
            return usuario != null ? _mapper.Map<UsuarioResponse>(usuario) : null;
        }

        public async Task<UsuarioResponse?> AtualizarAsync(string id, UsuarioUpdateRequest request)
        {
            var usuarioExistente = await _usuarioRepository.ObterPorIdAsync(id);
            if (usuarioExistente == null) return null;

            // Atualiza os campos permitidos
            usuarioExistente.Nome = request.Nome;
            usuarioExistente.Email = request.Email;
            if (!string.IsNullOrEmpty(request.Senha))
                usuarioExistente.Senha = request.Senha;

            await _usuarioRepository.AtualizarAsync(usuarioExistente);

            return _mapper.Map<UsuarioResponse>(usuarioExistente);
        }

        public async Task<bool> DeletarAsync(string id)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);
            if (usuario == null) return false;

            await _usuarioRepository.DeletarAsync(id);
            return true;
        }
    }
}

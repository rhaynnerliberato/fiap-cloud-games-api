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
        private readonly ILogger<UsuarioService> _logger;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, ILogger<UsuarioService> logger)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UsuarioResponse> CadastrarAsync(UsuarioCreateRequest request)
        {
            _logger.LogInformation("Iniciando cadastro de novo usuário com e-mail: {Email}", request.Email);

            var usuario = _mapper.Map<Usuario>(request);

            usuario.Perfil = request.Email.EndsWith("@fiap.com", StringComparison.OrdinalIgnoreCase)
                ? PerfilUsuario.Administrador
                : PerfilUsuario.Jogador;

            await _usuarioRepository.CadastrarAsync(usuario);

            _logger.LogInformation("Usuário cadastrado com sucesso. ID: {Id}, Perfil: {Perfil}", usuario.Id, usuario.Perfil.ToString());

            var response = _mapper.Map<UsuarioResponse>(usuario);
            return response;
        }

        public async Task<Usuario?> AutenticarAsync(LoginRequest dto)
        {
            _logger.LogInformation("Tentativa de autenticação para o e-mail: {Email}", dto.Email);
            var usuario = await _usuarioRepository.ObterPorEmailAsync(dto.Email);

            if (usuario == null)
                _logger.LogWarning("Usuário não encontrado para o e-mail: {Email}", dto.Email);
            else
                _logger.LogInformation("Usuário localizado para o e-mail: {Email}", dto.Email);

            return usuario;
        }

        public async Task<IEnumerable<UsuarioResponse>> ObterTodosAsync()
        {
            _logger.LogInformation("Buscando todos os usuários.");
            var usuarios = await _usuarioRepository.ObterTodosAsync();
            _logger.LogInformation("Total de usuários encontrados: {Quantidade}", usuarios.Count());
            return usuarios.Select(u => _mapper.Map<UsuarioResponse>(u));
        }

        public async Task<UsuarioResponse?> ObterPorIdAsync(string id)
        {
            _logger.LogInformation("Buscando usuário com ID: {Id}", id);
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);

            if (usuario == null)
            {
                _logger.LogWarning("Usuário com ID {Id} não encontrado.", id);
                return null;
            }

            _logger.LogInformation("Usuário com ID {Id} encontrado.", id);
            return _mapper.Map<UsuarioResponse>(usuario);
        }

        public async Task<UsuarioResponse?> AtualizarAsync(string id, UsuarioUpdateRequest request)
        {
            _logger.LogInformation("Atualizando usuário com ID: {Id}", id);
            var usuarioExistente = await _usuarioRepository.ObterPorIdAsync(id);

            if (usuarioExistente == null)
            {
                _logger.LogWarning("Usuário com ID {Id} não encontrado para atualização.", id);
                return null;
            }

            usuarioExistente.Nome = request.Nome;
            usuarioExistente.Email = request.Email;
            if (!string.IsNullOrEmpty(request.Senha))
                usuarioExistente.Senha = request.Senha;

            await _usuarioRepository.AtualizarAsync(usuarioExistente);

            _logger.LogInformation("Usuário com ID {Id} atualizado com sucesso.", id);
            return _mapper.Map<UsuarioResponse>(usuarioExistente);
        }

        public async Task<bool> DeletarAsync(string id)
        {
            _logger.LogInformation("Tentando deletar usuário com ID: {Id}", id);
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);

            if (usuario == null)
            {
                _logger.LogWarning("Usuário com ID {Id} não encontrado para deleção.", id);
                return false;
            }

            await _usuarioRepository.DeletarAsync(id);
            _logger.LogInformation("Usuário com ID {Id} deletado com sucesso.", id);
            return true;
        }
    }
}

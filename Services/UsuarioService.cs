using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games.Domain.Enums;
using fiap_cloud_games.Domain.Interfaces;
using fiap_cloud_games_api.Requests;
using fiap_cloud_games_api.Responses;
using AutoMapper;
using System.Threading.Tasks;
using fiap_cloud_games_api.LoginRequests;

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
            // Mapear request para entidade
            var usuario = _mapper.Map<Usuario>(request);

            // Definir perfil com base no domínio do email
            usuario.Perfil = request.Email.EndsWith("@fiap.com", StringComparison.OrdinalIgnoreCase)
                ? PerfilUsuario.Admin
                : PerfilUsuario.Jogador;

            await _usuarioRepository.CadastrarAsync(usuario);

            // Mapear entidade para response, omitindo a senha
            var response = _mapper.Map<UsuarioResponse>(usuario);
            return response;
        }

        public async Task<Usuario?> AutenticarAsync(LoginRequest dto)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(dto.Email);
            return usuario;
        }
    }
}

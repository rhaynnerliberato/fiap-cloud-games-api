using AutoMapper;
using fiap_cloud_games.API.DTOs.Responses;
using fiap_cloud_games.Application.DTOs.Requests;
using fiap_cloud_games.Application.Services;
using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games.Domain.Enums;
using fiap_cloud_games.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace fiap_cloud_games.Tests.Services
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<UsuarioService>> _loggerMock;
        private readonly UsuarioService _usuarioService;

        public UsuarioServiceTests()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<UsuarioService>>();
            _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CadastrarAsync_EmailFiap_AssignsAdministradorPerfil()
        {
            var request = new Usuario { Email = "admin@fiap.com", Nome = "Admin", Senha = "123" };
            var usuario = new Usuario { Email = request.Email };
            var response = new UsuarioResponse { Email = request.Email };

            _mapperMock.Setup(m => m.Map<Usuario>(request)).Returns(usuario);
            _mapperMock.Setup(m => m.Map<UsuarioResponse>(usuario)).Returns(response);

            var result = await _usuarioService.CadastrarAsync(request);

            _usuarioRepositoryMock.Verify(r => r.CadastrarAsync(usuario), Times.Once);
            Assert.Equal(PerfilUsuario.Administrador, usuario.Perfil);
            Assert.Equal(response, result);
        }

        [Fact]
        public async Task AutenticarAsync_UsuarioExiste_RetornaUsuario()
        {
            var email = "user@teste.com";
            var usuario = new Usuario { Email = email };
            _usuarioRepositoryMock.Setup(r => r.ObterPorEmailAsync(email)).ReturnsAsync(usuario);

            var result = await _usuarioService.AutenticarAsync(new LoginRequest { Email = email });

            Assert.NotNull(result);
            Assert.Equal(email, result?.Email);
        }

        [Fact]
        public async Task AutenticarAsync_UsuarioNaoExiste_RetornaNull()
        {
            var email = "naoexiste@teste.com";
            _usuarioRepositoryMock.Setup(r => r.ObterPorEmailAsync(email)).ReturnsAsync((Usuario?)null);

            var result = await _usuarioService.AutenticarAsync(new LoginRequest { Email = email });

            Assert.Null(result);
        }

        [Fact]
        public async Task ObterTodosAsync_RetornaListaDeUsuarioResponse()
        {
            var usuarios = new List<Usuario> { new Usuario { Email = "teste@teste.com" } };
            var response = new UsuarioResponse { Email = "teste@teste.com" };
            _usuarioRepositoryMock.Setup(r => r.ObterTodosAsync()).ReturnsAsync(usuarios);
            _mapperMock.Setup(m => m.Map<UsuarioResponse>(usuarios[0])).Returns(response);

            var result = await _usuarioService.ObterTodosAsync();

            Assert.Single(result);
            Assert.Equal("teste@teste.com", result.First().Email);
        }

        [Fact]
        public async Task ObterPorIdAsync_UsuarioExiste_RetornaUsuarioResponse()
        {
            var id = "507f1f77bcf86cd799439011";
            var usuario = new Usuario { Id = new MongoDB.Bson.ObjectId(id), Email = "teste@teste.com" };
            var response = new UsuarioResponse { Email = usuario.Email };
            _usuarioRepositoryMock.Setup(r => r.ObterPorIdAsync(id)).ReturnsAsync(usuario);
            _mapperMock.Setup(m => m.Map<UsuarioResponse>(usuario)).Returns(response);

            var result = await _usuarioService.ObterPorIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(usuario.Email, result?.Email);
        }

        [Fact]
        public async Task ObterPorIdAsync_UsuarioNaoExiste_RetornaNull()
        {
            var id = "naoexiste";
            _usuarioRepositoryMock.Setup(r => r.ObterPorIdAsync(id)).ReturnsAsync((Usuario?)null);

            var result = await _usuarioService.ObterPorIdAsync(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task AtualizarAsync_UsuarioExiste_AtualizaRetornaResponse()
        {
            var id = "507f1f77bcf86cd799439011";
            var request = new Usuario { Nome = "Novo Nome", Email = "novo@email.com", Senha = "novaSenha" };
            var usuario = new Usuario { Id = new MongoDB.Bson.ObjectId(id), Nome = "Antigo", Email = "antigo@email.com", Senha = "123" };
            var response = new UsuarioResponse { Email = request.Email };

            _usuarioRepositoryMock.Setup(r => r.ObterPorIdAsync(id)).ReturnsAsync(usuario);
            _mapperMock.Setup(m => m.Map<UsuarioResponse>(usuario)).Returns(response);

            var result = await _usuarioService.AtualizarAsync(id, request);

            _usuarioRepositoryMock.Verify(r => r.AtualizarAsync(usuario), Times.Once);
            Assert.Equal(request.Email, result?.Email);
        }

        [Fact]
        public async Task AtualizarAsync_UsuarioNaoExiste_RetornaNull()
        {
            var id = "naoexiste";
            var request = new Usuario { Nome = "Nome", Email = "email@email.com" };
            _usuarioRepositoryMock.Setup(r => r.ObterPorIdAsync(id)).ReturnsAsync((Usuario?)null);

            var result = await _usuarioService.AtualizarAsync(id, request);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeletarAsync_UsuarioExiste_DeletaRetornaTrue()
        {
            var id = "507f1f77bcf86cd799439011";
            var usuario = new Usuario { Id = new MongoDB.Bson.ObjectId(id) };
            _usuarioRepositoryMock.Setup(r => r.ObterPorIdAsync(id)).ReturnsAsync(usuario);

            var result = await _usuarioService.DeletarAsync(id);

            _usuarioRepositoryMock.Verify(r => r.DeletarAsync(id), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task DeletarAsync_UsuarioNaoExiste_RetornaFalse()
        {
            var id = "naoexiste";
            _usuarioRepositoryMock.Setup(r => r.ObterPorIdAsync(id)).ReturnsAsync((Usuario?)null);

            var result = await _usuarioService.DeletarAsync(id);

            Assert.False(result);
        }
    }
}

using fiap_cloud_games.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using MongoDB.Bson;
using fiap_cloud_games.Domain.Interfaces.Repositories;
using fiap_cloud_games.Application.Services;
using AutoMapper;
using fiap_cloud_games.API.DTOs.Responses;

namespace fiap_cloud_games.Tests.Services
{
    public class JogoServiceTests
    {
        private readonly Mock<IJogoRepository> _jogoRepositoryMock;
        private readonly Mock<ILogger<JogoService>> _loggerMock;
        private readonly JogoService _jogoService;
        private readonly Mock<IMapper> _mapperMock;

        public JogoServiceTests()
        {
            _jogoRepositoryMock = new Mock<IJogoRepository>();
            _loggerMock = new Mock<ILogger<JogoService>>();
            _mapperMock = new Mock<IMapper>();
            _jogoService = new JogoService(_jogoRepositoryMock.Object, _loggerMock.Object, _mapperMock.Object);

        }

        [Fact]
        public async Task ListarAsync_ReturnsJogosList()
        {
            // Arrange
            var jogos = new List<Jogo>
            {
                new Jogo { Id = ObjectId.GenerateNewId(), Nome = "Jogo 1", Valor = 100 }
            };

            var jogosResponse = new List<JogoResponse>
            {
                new JogoResponse { Id = jogos[0].Id, Nome = "Jogo 1", Valor = 100 }
            };

            _jogoRepositoryMock.Setup(repo => repo.ListarAsync()).ReturnsAsync(jogos);
            _mapperMock.Setup(m => m.Map<List<JogoResponse>>(jogos)).Returns(jogosResponse);

            // Act
            var result = await _jogoService.ListarAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Jogo 1", result[0].Nome);
        }


        [Fact]
        public async Task ObterPorIdAsync_ExistingId_ReturnsJogo()
        {
            var id = ObjectId.GenerateNewId();
            var jogo = new Jogo { Id = id, Nome = "Jogo Teste" };

            _jogoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(id.ToString())).ReturnsAsync(jogo);

            var result = await _jogoService.ObterPorIdAsync(id.ToString());

            Assert.NotNull(result);
            Assert.Equal("Jogo Teste", result.Nome);
        }

        [Fact]
        public async Task ObterPorIdAsync_NonExistingId_ReturnsNull()
        {
            var id = ObjectId.GenerateNewId().ToString();

            _jogoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(id)).ReturnsAsync((Jogo)null);

            var result = await _jogoService.ObterPorIdAsync(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task CadastrarAsync_ValidJogo_ReturnsJogoResponse()
        {
            // Arrange
            var jogo = new Jogo { Id = ObjectId.GenerateNewId(), Nome = "Novo Jogo" };

            var jogoResponse = new JogoResponse { Id = jogo.Id, Nome = jogo.Nome };

            _jogoRepositoryMock.Setup(r => r.CadastrarAsync(jogo)).ReturnsAsync(jogo);

            _mapperMock.Setup(m => m.Map<JogoResponse>(jogo)).Returns(jogoResponse);

            // Act
            var result = await _jogoService.CadastrarAsync(jogo);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(jogo.Id, result.Id);
            Assert.Equal(jogo.Nome, result.Nome);
        }


        [Fact]
        public async Task AtualizarAsync_ValidJogo_CallsRepository()
        {
            var jogo = new Jogo { Id = ObjectId.GenerateNewId(), Nome = "Atualizado" };

            _jogoRepositoryMock.Setup(repo => repo.AtualizarAsync(jogo)).Returns(Task.CompletedTask);

            await _jogoService.AtualizarAsync(jogo);

            _jogoRepositoryMock.Verify(repo => repo.AtualizarAsync(jogo), Times.Once);
        }

        [Fact]
        public async Task DeletarAsync_ValidJogo_CallsRepository()
        {
            var jogo = new Jogo { Id = ObjectId.GenerateNewId(), Nome = "Para Deletar" };

            _jogoRepositoryMock.Setup(repo => repo.DeletarAsync(jogo)).Returns(Task.CompletedTask);

            await _jogoService.DeletarAsync(jogo);

            _jogoRepositoryMock.Verify(repo => repo.DeletarAsync(jogo), Times.Once);
        }

        [Fact]
        public async Task AplicarPromocaoAsync_ValidId_AppliesDiscount()
        {
            var id = ObjectId.GenerateNewId();
            var jogo = new Jogo { Id = id, Nome = "Promo", Valor = 200M };

            _jogoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(id.ToString())).ReturnsAsync(jogo);
            _jogoRepositoryMock.Setup(repo => repo.AtualizarAsync(It.IsAny<Jogo>())).Returns(Task.CompletedTask);

            var result = await _jogoService.AplicarPromocaoAsync(id.ToString(), 10);

            Assert.True(result);
            Assert.Equal(180M, jogo.Valor); // 10% de desconto
        }

        [Fact]
        public async Task AplicarPromocaoAsync_InvalidId_ReturnsFalse()
        {
            var id = ObjectId.GenerateNewId().ToString();

            _jogoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(id)).ReturnsAsync((Jogo)null);

            var result = await _jogoService.AplicarPromocaoAsync(id, 10);

            Assert.False(result);
        }
    }
}

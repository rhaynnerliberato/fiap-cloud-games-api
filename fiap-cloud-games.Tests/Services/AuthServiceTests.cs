﻿using Moq;
using fiap_cloud_games.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using fiap_cloud_games.Domain.Interfaces.Repositories;
using fiap_cloud_games.Application.Services;
using fiap_cloud_games.Application.DTOs.Requests;
using fiap_cloud_games.Domain.ValueObjects;

namespace fiap_cloud_games.Tests.Services
{
    public class AuthServiceTests
    {
        [Fact]
        public async Task Authenticate_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var usuarioMock = new Usuario
            {
                Email = "admin@fiap.com",
                Senha = "asDF234@#$",
                Perfil = Domain.Enums.PerfilUsuario.Administrador
            };

            var repoMock = new Mock<IUsuarioRepository>();
            repoMock.Setup(r => r.ObterPorEmailAsync("admin@fiap.com"))
                    .ReturnsAsync(usuarioMock);

            var configMock = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Jwt:Key", "fsklj34859!@#$@R@Fsdgf#@$RWEGRTh5" }
                })
                .Build();

            var loggerMock = new Mock<ILogger<AuthService>>();

            var service = new AuthService(configMock, repoMock.Object, loggerMock.Object);

            var loginRequest = new AuthCredentials
            {
                Email = "admin@fiap.com",
                Senha = "asDF234@#$"
            };

            // Act
            var token = await service.Authenticate(loginRequest);

            // Assert
            Assert.NotNull(token);
            Assert.IsType<string>(token);
            Assert.True(token.Length > 0);
        }
    }
}

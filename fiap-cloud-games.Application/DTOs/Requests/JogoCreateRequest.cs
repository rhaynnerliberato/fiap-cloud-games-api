﻿namespace fiap_cloud_games.Application.DTOs.Requests
{
    public class JogoCreateRequest
    {
        public required string Nome { get; set; }
        public required string Plataforma { get; set; }
        public required string Genero { get; set; }
        public string? Descricao { get; set; }
        public required decimal Valor { get; set; }
    }
}

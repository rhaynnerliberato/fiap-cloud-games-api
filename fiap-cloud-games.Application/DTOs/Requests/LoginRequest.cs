﻿namespace fiap_cloud_games.Application.DTOs.Requests
{
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}

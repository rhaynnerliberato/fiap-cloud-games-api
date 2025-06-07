using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games.API.DTOs.Responses;
using fiap_cloud_games.Domain.ValueObjects;
using AutoMapper;
using fiap_cloud_games.Application.DTOs.Requests;

namespace fiap_cloud_games.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Jogos
            CreateMap<Jogo, JogoRequest>();
            CreateMap<Jogo, JogoResponse>();
            CreateMap<JogoCreateRequest, Jogo>();
            CreateMap<JogoUpdateRequest, Jogo>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Usuarios
            CreateMap<UsuarioCreateRequest, Usuario>();
            CreateMap<Usuario, UsuarioResponse>()
                .ForMember(dest => dest.Perfil, opt => opt.MapFrom(src => src.Perfil.ToString()));
            CreateMap<UsuarioUpdateRequest, Usuario>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            
            // Login
            CreateMap<LoginRequest, AuthCredentials>();
        }
    }
}

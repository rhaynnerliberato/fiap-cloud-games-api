using AutoMapper;
using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games_api.Models.Requests;
using fiap_cloud_games_api.Models.Responses;

namespace fiap_cloud_games_api.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Jogo, JogoRequest>();
            CreateMap<JogoCreateRequest, Jogo>();
            CreateMap<JogoUpdateRequest, Jogo>();

            CreateMap<UsuarioCreateRequest, Usuario>();
            CreateMap<Usuario, UsuarioResponse>();


        }
    }
}

using AutoMapper;
using fiap_cloud_games.Domain.Entities;
using fiap_cloud_games_api.DTOs;

namespace fiap_cloud_games_api.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Jogo, JogoDTO>();
            CreateMap<JogoCreateDTO, Jogo>();
            CreateMap<JogoUpdateDTO, Jogo>();


        }
    }
}

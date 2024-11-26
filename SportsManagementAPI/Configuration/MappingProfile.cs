using AutoMapper;
using SportsManagementAPI.Data.Models;
using SportsManagementAPI.Models;

namespace SportsManagementAPI.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PlayerRequestDto, Player>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Player, PlayerResponseDto>().ReverseMap();

            CreateMap<LeagueRequestDto, League>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<League, LeagueResponseDto>().ReverseMap();

            CreateMap<TeamRequestDto, Team>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Team, TeamResponseDto>().ReverseMap();

            CreateMap<MatchRequestDto, Match>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Match, MatchResponseDto>().ReverseMap();
        }
    }
}

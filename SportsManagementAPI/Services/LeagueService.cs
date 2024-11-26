using AutoMapper;
using SportsManagementAPI.Core.Repositories;
using SportsManagementAPI.Data.Models;
using SportsManagementAPI.Models;

namespace SportsManagementAPI.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly ILeagueRepository _leagueRepository;
        private readonly IMapper _mapper;
        public LeagueService(ILeagueRepository leagueRepository, IMapper mapper)
        {
            _leagueRepository = leagueRepository;
            _mapper = mapper;
        }

        public async Task<LeagueResponseDto> GetLeagueByIdAsync(int id)
        {
            var league = await _leagueRepository.GetByIdAsync(id);

            if (league == null) throw new KeyNotFoundException("League not found");

            var leagueDto = _mapper.Map<LeagueResponseDto>(league);
            return leagueDto;
        }
        public async Task<LeagueResponseDto> AddLeagueAsync(LeagueRequestDto leagueDto)
        {
            var league = _mapper.Map<League>(leagueDto);

            await _leagueRepository.AddAsync(league);

            var leagueResponse = _mapper.Map<LeagueResponseDto>(league);

            return leagueResponse;
        }
        public async Task UpdateLeagueAsync(int leagueId, LeagueRequestDto leagueDto)
        {
            var existingLeague = await _leagueRepository.GetByIdAsync(leagueId);
            if (existingLeague == null) throw new KeyNotFoundException("League not found");

            _mapper.Map(leagueDto, existingLeague);

            await _leagueRepository.UpdateAsync(existingLeague);
        }

        public async Task DeleteLeagueAsync(int id)
        {
            var league = await _leagueRepository.GetByIdAsync(id);

            if (league == null) throw new KeyNotFoundException("League not found");

            await _leagueRepository.DeleteAsync(league);
        }
    }
}

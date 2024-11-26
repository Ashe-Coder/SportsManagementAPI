using AutoMapper;
using SportsManagementAPI.Core.Repositories;
using SportsManagementAPI.Data.Models;
using SportsManagementAPI.Models;

namespace SportsManagementAPI.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ILeagueRepository _leagueRepository;
        private readonly IMapper _mapper;
        public TeamService(ITeamRepository teamRepository, ILeagueRepository leagueRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _leagueRepository = leagueRepository;
            _mapper = mapper;
        }

        public async Task<TeamResponseDto> GetTeamByIdAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);

            if (team == null) throw new KeyNotFoundException("Team not found");

            var teamDto = _mapper.Map<TeamResponseDto>(team);
            return teamDto;
        }
        public async Task<TeamResponseDto> AddTeamAsync(TeamRequestDto teamDto)
        {
            var team = _mapper.Map<Team>(teamDto);

            await ValidateLeagueExistenceAsync(team.LeagueId);

            await _teamRepository.AddAsync(team);

            var teamResponse = _mapper.Map<TeamResponseDto>(team);

            return teamResponse;
        }
        public async Task UpdateTeamAsync(int teamId, TeamRequestDto teamDto)
        {
            var existingTeam = await _teamRepository.GetByIdAsync(teamId);
            if (existingTeam == null) throw new KeyNotFoundException("Team not found");

            await ValidateLeagueExistenceAsync(teamDto.LeagueId);

            _mapper.Map(teamDto, existingTeam);

            await _teamRepository.UpdateAsync(existingTeam);
        }

        public async Task DeleteTeamAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);

            if (team == null) throw new KeyNotFoundException("Team not found");

            await _teamRepository.DeleteAsync(team);
        }

        private async Task ValidateLeagueExistenceAsync(int? leagueId)
        {
            if (leagueId.HasValue)
            {
                var league = await _leagueRepository.GetByIdAsync(leagueId.Value);
                if (league == null)
                {
                    throw new KeyNotFoundException("league not found");
                }
            }
        }

    }
}

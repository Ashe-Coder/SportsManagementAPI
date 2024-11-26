using AutoMapper;
using SportsManagementAPI.Core.Repositories;
using SportsManagementAPI.Data.Models;
using SportsManagementAPI.Models;

namespace SportsManagementAPI.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        public MatchService(IMatchRepository matchRepository, ITeamRepository teamRepository, IMapper mapper)
        {
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        public async Task<MatchResponseDto> GetMatchByIdAsync(int id)
        {
            var match = await _matchRepository.GetByIdAsync(id);

            if (match == null) throw new KeyNotFoundException("Match not found");

            var matchDto = _mapper.Map<MatchResponseDto>(match);
            return matchDto;
        }
        public async Task<MatchResponseDto> AddMatchAsync(MatchRequestDto matchDto)
        {
            var match = _mapper.Map<Match>(matchDto);

            await ValidateTeamExistenceAsync(matchDto.HomeTeamId);
            await ValidateTeamExistenceAsync(matchDto.AwayTeamId);

            await _matchRepository.AddAsync(match);

            var matchResponse = _mapper.Map<MatchResponseDto>(match);

            return matchResponse;
        }
        public async Task UpdateMatchAsync(int matchId, MatchRequestDto matchDto)
        {
            var existingMatch = await _matchRepository.GetByIdAsync(matchId);
            if (existingMatch == null) throw new KeyNotFoundException("Match not found");

            await ValidateTeamExistenceAsync(matchDto.HomeTeamId);
            await ValidateTeamExistenceAsync(matchDto.AwayTeamId);

            _mapper.Map(matchDto, existingMatch);

            await _matchRepository.UpdateAsync(existingMatch);
        }

        public async Task DeleteMatchAsync(int id)
        {
            var match = await _matchRepository.GetByIdAsync(id);

            if (match == null) throw new KeyNotFoundException("Match not found");

            await _matchRepository.DeleteAsync(match);
        }

        private async Task ValidateTeamExistenceAsync(int? teamId)
        {
            if (teamId.HasValue)
            {
                var team = await _teamRepository.GetByIdAsync(teamId.Value);
                if (team == null)
                {
                    throw new KeyNotFoundException("Team not found");
                }
            }
        }

    }
}

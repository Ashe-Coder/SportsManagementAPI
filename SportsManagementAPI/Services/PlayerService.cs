using AutoMapper;
using SportsManagementAPI.Core.Repositories;
using SportsManagementAPI.Data.Models;
using SportsManagementAPI.Models;

namespace SportsManagementAPI.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        public PlayerService(IPlayerRepository playerRepository, ITeamRepository teamRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        public async Task<PlayerResponseDto> GetPlayerByIdAsync(int id)
        {
            var player = await _playerRepository.GetByIdAsync(id);

            if (player == null) throw new KeyNotFoundException("Player not found");

            var playerDto = _mapper.Map<PlayerResponseDto>(player);
            return playerDto;
        }
        public async Task<PlayerResponseDto> AddPlayerAsync(PlayerRequestDto playerDto)
        {
            var player = _mapper.Map<Player>(playerDto);

            await ValidateTeamExistenceAsync(player.TeamId);

            await _playerRepository.AddAsync(player);

            var playerResponse = _mapper.Map<PlayerResponseDto>(player);

            return playerResponse;
        }
        public async Task UpdatePlayerAsync(int playerId, PlayerRequestDto playerDto)
        {
            var existingPlayer = await _playerRepository.GetByIdAsync(playerId);
            if (existingPlayer == null) throw new KeyNotFoundException("Player not found");

            await ValidateTeamExistenceAsync(playerDto.TeamId);

            _mapper.Map(playerDto, existingPlayer);

            await _playerRepository.UpdateAsync(existingPlayer);
        }

        public async Task DeletePlayerAsync(int id)
        {
            var player = await _playerRepository.GetByIdAsync(id);

            if (player == null) throw new KeyNotFoundException("Player not found");

            await _playerRepository.DeleteAsync(player);
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

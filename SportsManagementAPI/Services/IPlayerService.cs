using SportsManagementAPI.Models;

namespace SportsManagementAPI.Services
{
    public interface IPlayerService
    {
        Task<PlayerResponseDto> GetPlayerByIdAsync(int id);
        Task<PlayerResponseDto> AddPlayerAsync(PlayerRequestDto player);
        Task UpdatePlayerAsync(int playerId, PlayerRequestDto player);
        Task DeletePlayerAsync(int id);
    }
}

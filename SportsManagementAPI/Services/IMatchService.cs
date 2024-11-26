using SportsManagementAPI.Models;

namespace SportsManagementAPI.Services
{
    public interface IMatchService
    {
        Task<MatchResponseDto> GetMatchByIdAsync(int id);
        Task<MatchResponseDto> AddMatchAsync(MatchRequestDto Match);
        Task UpdateMatchAsync(int MatchId, MatchRequestDto Match);
        Task DeleteMatchAsync(int id);
    }
}

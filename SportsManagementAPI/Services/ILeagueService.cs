using SportsManagementAPI.Models;

namespace SportsManagementAPI.Services
{
    public interface ILeagueService
    {
        Task<LeagueResponseDto> GetLeagueByIdAsync(int id);
        Task<LeagueResponseDto> AddLeagueAsync(LeagueRequestDto league);
        Task UpdateLeagueAsync(int leagueId, LeagueRequestDto league);
        Task DeleteLeagueAsync(int id);
    }
}

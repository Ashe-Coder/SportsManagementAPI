using SportsManagementAPI.Models;

namespace SportsManagementAPI.Services
{
    public interface ITeamService
    {
        Task<TeamResponseDto> GetTeamByIdAsync(int id);
        Task<TeamResponseDto> AddTeamAsync(TeamRequestDto team);
        Task UpdateTeamAsync(int teamId, TeamRequestDto team);
        Task DeleteTeamAsync(int id);
    }
}

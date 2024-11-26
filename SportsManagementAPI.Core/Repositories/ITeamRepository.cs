using SportsManagementAPI.Data.Models;

namespace SportsManagementAPI.Core.Repositories
{
    public interface ITeamRepository
    {
        Task<Team> GetByIdAsync(int id);
        Task AddAsync(Team team);
        Task UpdateAsync(Team team);
        Task DeleteAsync(Team team);
    }
}

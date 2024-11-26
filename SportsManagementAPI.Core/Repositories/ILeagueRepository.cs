using SportsManagementAPI.Data.Models;

namespace SportsManagementAPI.Core.Repositories
{
    public interface ILeagueRepository
    {
        Task<League> GetByIdAsync(int id);
        Task AddAsync(League league);
        Task UpdateAsync(League league);
        Task DeleteAsync(League league);
    }
}

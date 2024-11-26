using SportsManagementAPI.Data.Models;

namespace SportsManagementAPI.Core.Repositories
{
    public interface IMatchRepository
    {
        Task<Match> GetByIdAsync(int id);
        Task<IEnumerable<Match>> GetUnfilledMatchesAsync();
        Task AddAsync(Match match);
        Task UpdateAsync(Match match);
        Task DeleteAsync(Match match);
    }
}

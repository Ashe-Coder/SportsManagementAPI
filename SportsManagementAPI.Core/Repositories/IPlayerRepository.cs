using SportsManagementAPI.Data.Models;

namespace SportsManagementAPI.Core.Repositories
{
    public interface IPlayerRepository
    {
        Task<Player> GetByIdAsync(int id);
        Task AddAsync(Player player);
        Task UpdateAsync(Player player);
        Task DeleteAsync(Player player);
    }
}

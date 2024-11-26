using Microsoft.EntityFrameworkCore;
using SportsManagementAPI.Data.DbContexts;
using SportsManagementAPI.Data.Models;

namespace SportsManagementAPI.Core.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext _context;
        public PlayerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Player> GetByIdAsync(int id)
        {
            return await _context.Players.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Player player)
        {
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Player player)
        {
            _context.Players.Update(player);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Player player)
        {
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }
    }

}

using Microsoft.EntityFrameworkCore;
using SportsManagementAPI.Data.DbContexts;
using SportsManagementAPI.Data.Models;

namespace SportsManagementAPI.Core.Repositories
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly ApplicationDbContext _context;
        public LeagueRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<League> GetByIdAsync(int id)
        {
            return await _context.Leagues.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(League league)
        {
            await _context.Leagues.AddAsync(league);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(League league)
        {
            _context.Leagues.Update(league);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(League league)
        {
            _context.Leagues.Remove(league);
            await _context.SaveChangesAsync();
        }
    }

}

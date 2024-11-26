using Microsoft.EntityFrameworkCore;
using SportsManagementAPI.Data.DbContexts;
using SportsManagementAPI.Data.Models;

namespace SportsManagementAPI.Core.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly ApplicationDbContext _context;
        public MatchRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Match> GetByIdAsync(int id)
        {
            return await _context.Matches.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Match>> GetUnfilledMatchesAsync()
        {
            return await _context.Matches
                .Where(m => m.TotalPasses == null || m.TotalPasses == 0)  
                .ToListAsync();
        }

        public async Task AddAsync(Match team)
        {
            await _context.Matches.AddAsync(team);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Match match)
        {
            _context.Matches.Update(match);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Match team)
        {
            _context.Matches.Remove(team);
            await _context.SaveChangesAsync();
        }


    }

}

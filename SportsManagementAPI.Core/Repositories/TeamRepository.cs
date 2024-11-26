using Microsoft.EntityFrameworkCore;
using SportsManagementAPI.Data.DbContexts;
using SportsManagementAPI.Data.Models;

namespace SportsManagementAPI.Core.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _context;
        public TeamRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Team> GetByIdAsync(int id)
        {
            return await _context.Teams.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Team team)
        {
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Team team)
        {
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Team team)
        {
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }
    }

}

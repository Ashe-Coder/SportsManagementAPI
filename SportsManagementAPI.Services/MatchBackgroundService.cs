using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using SportsManagementAPI.Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SportsManagementServices.Services
{
    public class MatchBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Random _random;
        private readonly ILogger<MatchBackgroundService> _logger;

        public MatchBackgroundService(IServiceScopeFactory scopeFactory, ILogger<MatchBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _random = new Random();
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background service started at {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); 

                _logger.LogInformation("Running background task to update TotalPasses at {time}", DateTimeOffset.Now);

                try
                {
                    await UpdateTotalPassesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while updating TotalPasses");
                }
            }
        }

        public async Task UpdateTotalPassesAsync()
        {
            // Create a scope to resolve scoped services (IMatchRepository)
            using (var scope = _scopeFactory.CreateScope())
            {
                var matchRepository = scope.ServiceProvider.GetRequiredService<IMatchRepository>();
                var matches = await matchRepository.GetUnfilledMatchesAsync();
                _logger.LogInformation("{count} matches found to update", matches.Count());

                foreach (var match in matches)
                {
                    _logger.LogInformation("Updating match {matchId}", match.Id);
                    match.TotalPasses = _random.Next(100, 1001);  
                    await matchRepository.UpdateAsync(match);    
                }

                _logger.LogInformation("{count} matches updated with new TotalPasses", matches.Count());
            }
        }
    }
}

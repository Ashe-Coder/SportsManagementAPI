using Moq;
using Microsoft.Extensions.Logging;
using SportsManagementAPI.Core.Repositories;
using SportsManagementAPI.Data.Models;
using SportsManagementAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using SportsManagementServices.Services;
using Match = SportsManagementAPI.Data.Models.Match;

namespace SportsManagementAPI.Tests
{
    public class MatchBackgroundServiceTests
    {
        private readonly Mock<IServiceScopeFactory> _mockScopeFactory;
        private readonly Mock<IMatchRepository> _mockMatchRepository;
        private readonly Mock<ILogger<MatchBackgroundService>> _mockLogger;
        private readonly MatchBackgroundService _backgroundService;

        public MatchBackgroundServiceTests()
        {
            _mockScopeFactory = new Mock<IServiceScopeFactory>();
            _mockMatchRepository = new Mock<IMatchRepository>();
            _mockLogger = new Mock<ILogger<MatchBackgroundService>>();
            _backgroundService = new MatchBackgroundService(_mockScopeFactory.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task UpdateTotalPassesAsync_UpdatesTotalPassesForUnfilledMatches()
        {
            // Arrange
            var matches = new List<Match>
            {
                new Match { Id = 1, HomeTeamId = 1, AwayTeamId = 2, TotalPasses = 0 },
                new Match { Id = 2, HomeTeamId = 3, AwayTeamId = 4, TotalPasses = null }
            };

            var mockScope = new Mock<IServiceScope>();

            mockScope.Setup(scope => scope.ServiceProvider.GetService(typeof(IMatchRepository)))
                     .Returns(_mockMatchRepository.Object);

            _mockScopeFactory.Setup(factory => factory.CreateScope()).Returns(mockScope.Object);

            _mockMatchRepository.Setup(repo => repo.GetUnfilledMatchesAsync()).ReturnsAsync(matches);

            // Act
            await _backgroundService.UpdateTotalPassesAsync();  // Call the public method directly

            // Assert
            _mockMatchRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Match>()), Times.Exactly(2));
        }
    }
}

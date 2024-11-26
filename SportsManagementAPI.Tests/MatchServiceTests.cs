using AutoMapper;
using Moq;
using SportsManagementAPI.Core.Repositories;
using SportsManagementAPI.Data.Models;
using SportsManagementAPI.Models;
using SportsManagementAPI.Services;
using Xunit;
using Match = SportsManagementAPI.Data.Models.Match;

namespace SportsManagementAPI.Tests
{
    public class MatchServiceTests
    {
        [Fact]
        public async Task AddMatchAsync_ReturnsMatchResponseDto_WhenMatchIsAdded()
        {
            // Arrange
            var mockMatchRepository = new Mock<IMatchRepository>();
            var mockTeamRepository = new Mock<ITeamRepository>();
            var mockMapper = new Mock<IMapper>();

            var matchDto = new MatchRequestDto { HomeTeamId = 1, AwayTeamId = 2 };
            var match = new Match { Id = 1, HomeTeamId = 1, AwayTeamId = 2 };
            var matchResponseDto = new MatchResponseDto { Id = 1, HomeTeamId = 1, AwayTeamId = 2 };

            var homeTeam = new Team { Id = 1, Name = "Team A" };
            var awayTeam = new Team { Id = 2, Name = "Team B" };

            mockTeamRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(homeTeam);
            mockTeamRepository.Setup(repo => repo.GetByIdAsync(2)).ReturnsAsync(awayTeam);

            mockMapper.Setup(m => m.Map<Match>(It.IsAny<MatchRequestDto>())).Returns(match);
            mockMapper.Setup(m => m.Map<MatchResponseDto>(It.IsAny<Match>())).Returns(matchResponseDto);

            var service = new MatchService(mockMatchRepository.Object, mockTeamRepository.Object, mockMapper.Object);

            // Act
            var result = await service.AddMatchAsync(matchDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(1, result.HomeTeamId);
            Assert.Equal(2, result.AwayTeamId);
        }
    }
}

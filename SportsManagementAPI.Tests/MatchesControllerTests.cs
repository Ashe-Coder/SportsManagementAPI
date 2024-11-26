using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsManagementAPI.Controllers;
using SportsManagementAPI.Models;
using SportsManagementAPI.Services;
using Xunit;

namespace SportsManagementAPI.Tests
{
    public class MatchesControllerTests
    {
        [Fact]
        public async Task GetMatch_ReturnsOkResult_WhenMatchExists()
        {
            // Arrange
            var mockMatchService = new Mock<IMatchService>();
            var matchResponseDto = new MatchResponseDto { Id = 1, HomeTeamId = 1, AwayTeamId = 2 };
            mockMatchService.Setup(service => service.GetMatchByIdAsync(It.IsAny<int>()))
                            .ReturnsAsync(matchResponseDto);

            var controller = new MatchesController(mockMatchService.Object);

            // Act
            var result = await controller.GetMatch(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<MatchResponseDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task AddMatch_ReturnsBadRequest_WhenMatchDataIsInvalid()
        {
            // Arrange
            var mockMatchService = new Mock<IMatchService>();
            var controller = new MatchesController(mockMatchService.Object);
            controller.ModelState.AddModelError("HomeTeamId", "Required");

            // Act
            var result = await controller.AddMatch(new MatchRequestDto());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateMatch_ReturnsNoContent_WhenMatchIsUpdated()
        {
            // Arrange
            var mockMatchService = new Mock<IMatchService>();
            mockMatchService.Setup(service => service.UpdateMatchAsync(It.IsAny<int>(), It.IsAny<MatchRequestDto>()))
                            .Returns(Task.CompletedTask);

            var controller = new MatchesController(mockMatchService.Object);

            // Act
            var result = await controller.UpdateMatch(1, new MatchRequestDto());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteMatch_ReturnsNoContent_WhenMatchIsDeleted()
        {
            // Arrange
            var mockMatchService = new Mock<IMatchService>();
            mockMatchService.Setup(service => service.DeleteMatchAsync(It.IsAny<int>()))
                            .Returns(Task.CompletedTask);

            var controller = new MatchesController(mockMatchService.Object);

            // Act
            var result = await controller.DeleteMatch(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteMatch_ReturnsNotFound_WhenMatchDoesNotExist()
        {
            // Arrange
            var mockMatchService = new Mock<IMatchService>();
            mockMatchService.Setup(service => service.DeleteMatchAsync(It.IsAny<int>()))
                            .ThrowsAsync(new KeyNotFoundException("Match not found"));

            var controller = new MatchesController(mockMatchService.Object);

            // Act
            var result = await controller.DeleteMatch(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("{ message = Match not found }", notFoundResult.Value.ToString());
        }
    }
}

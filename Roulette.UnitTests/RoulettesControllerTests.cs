using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Roulette.Api.Controllers;
using Roulette.Api.Dtos;
using Roulette.Api.Entities;
using Roulette.Api.Repositories;
using Xunit;

namespace Roulette.UnitTests
{
    public class RoulettesControllerTests
    {
        private readonly Mock<IIRoulettesRepository> repositoryStub = new();
        private readonly Random rand = new();
        [Fact]
        public async Task GetRouletteWheelAsync_WithUnexistingItem_ReturnsNotFound()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetRouletteWheelAsync(It.IsAny<Guid>()))
                .ReturnsAsync((RouletteWheel)null);

            var controller = new RoulettesController(repositoryStub.Object);

            // Act
            var result = await controller.GetRouletteWheelAsync(Guid.NewGuid());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public async Task GetRouletteWheelAsync_WithExistingItem_ReturnsExpectedItem()
        {
            // Arrange
            RouletteWheel expectedRouletteWheel = CreateRandomRouletteWheel();

            repositoryStub.Setup(repo => repo.GetRouletteWheelAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedRouletteWheel);

            var controller = new RoulettesController(repositoryStub.Object);

            // Act
            var result = await controller.GetRouletteWheelAsync(Guid.NewGuid());

            // Assert
            result.Value.Should().BeEquivalentTo(expectedRouletteWheel,
                                            options => options.ComparingByMembers<RouletteWheel>());
        }
        [Fact]
        public async Task GetRouletteWheelsAsync_WithExistingItems_ReturnsAllItems()
        {
            // Arrange
            var expectedRoulettes = new[] { CreateRandomRouletteWheel(), CreateRandomRouletteWheel(), CreateRandomRouletteWheel() };

            repositoryStub.Setup(repo => repo.GetRouletteWheelsAsync())
                .ReturnsAsync(expectedRoulettes);

            var controller = new RoulettesController(repositoryStub.Object);

            // Act
            var actualRouletteWheels = await controller.GetRouletteWheelsAsync();

            // Assert
            actualRouletteWheels.Should().BeEquivalentTo(expectedRoulettes,
                                                         options => options.ComparingByMembers<RouletteWheel>());
        }
        [Fact]
        public async Task CreateRouletteWheelAsync_WithItemToCreate_ReturnsCreatedItem()
        {
            // Arrange
            var controller = new RoulettesController(repositoryStub.Object);

            // Act
            var result = await controller.CreateRouletteWheelAsync();

            // Assert
            var createdItem = (result.Result as CreatedAtActionResult).Value as RouletteWheelDto;
            createdItem.Id.Should().NotBeEmpty();
            createdItem.CreatedDate.Should().BeCloseTo(DateTimeOffset.UtcNow, 1000);
        }
        [Fact]
        public async Task OpenItemAsync_WithExistingItem_ReturnsNoContent()
        {
            // Arrange
            RouletteWheel existingRouletteWheel = CreateRandomRouletteWheel();
            repositoryStub.Setup(repo => repo.GetRouletteWheelAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingRouletteWheel);

            var rouletteWheelid = existingRouletteWheel.Id;
            var rouletteWheelToUpdate = new RouletteWheel() { IsOpen = true };
            

            var controller = new RoulettesController(repositoryStub.Object);

            // Act
            var result = await controller.OpenRouletteWheelAsync(rouletteWheelid);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
        [Fact]
        public async Task CloseItemAsync_WithExistingItem_ReturnsNoContent()
        {
            // Arrange
            RouletteWheel existingRouletteWheel = CreateRandomRouletteWheel();
            repositoryStub.Setup(repo => repo.GetRouletteWheelAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingRouletteWheel);

            var rouletteWheelid = existingRouletteWheel.Id;
            var rouletteWheelToUpdate = new RouletteWheel() { IsOpen = false };            

            var controller = new RoulettesController(repositoryStub.Object);

            // Act
            var result = await controller.OpenRouletteWheelAsync(rouletteWheelid);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }                                           
        private RouletteWheel CreateRandomRouletteWheel()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                IsOpen = false,
                CreatedDate = DateTimeOffset.UtcNow
            };
        }
    }
}

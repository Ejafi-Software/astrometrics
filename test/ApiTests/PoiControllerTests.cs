using Bogus;
using Ejafi.Astrometrics.ApiService.Controllers;
using Ejafi.Astrometrics.ApiService.Data;
using Ejafi.Astrometrics.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace ApiTests;

public class PoiControllerTests
{
    public class CreationTests
    {
        [Fact]
        public async Task CreatePoi_WhenDoesNotExist_ShouldReturnOk()
        {
            // Arrange
            var poi = new Faker<PointOfInterest>()
                .RuleFor(p => p.Name, f => f.Lorem.Word());
            var mockRepo = Substitute.For<IPoiRepository>();
            mockRepo.ExistsAsync(poi)
                .Returns(false);

            var poiController = new PoiController(mockRepo);

            // Act
            var result = await poiController.CreatePoi(poi);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task CreatePoi_WhenPOIExists_ShouldReturnConflict()
        {
            // Arrange
            var poi = new Faker<PointOfInterest>()
                .RuleFor(p => p.Name, f => f.Lorem.Word());
            var mockRepo = Substitute.For<IPoiRepository>();
            mockRepo.ExistsAsync(poi)
                .Returns(true);

            var poiController = new PoiController(mockRepo);

            // Act
            var result = await poiController.CreatePoi(poi);

            // Assert
            result.Should().BeOfType<ConflictResult>();
        }

        [Fact]
        public async Task CreatePoi_WithMissingFields_ShouldReturnBadRequest()
        {
            // Arrange
            var poiRepository = Substitute.For<IPoiRepository>();
            var controller = new PoiController(poiRepository);
            var poi = new Faker<PointOfInterest>()
                .RuleFor(p => p.Name, string.Empty);

            // Act
            var result = await controller.CreatePoi(poi);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

    }
    public class ListTests
    {
        [Fact]
        public void ListPoisReturnsListOfPOIs()
        {
            // Arrange
            var pois = new List<PointOfInterest>
            {
                new() { Id = Guid.NewGuid(), Name = "POI 1" },
                new() { Id = Guid.NewGuid(), Name = "POI 2" },
                new() { Id = Guid.NewGuid(), Name = "POI 3" }
            };
            var mockRepo = Substitute.For<IPoiRepository>();
            mockRepo.ListAll().Returns(pois);

            var poiController = new PoiController(mockRepo);

            // Act
            var result = poiController.ListPois();

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(pois);
        }
    }
}

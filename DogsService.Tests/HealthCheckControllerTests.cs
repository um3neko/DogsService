
using Microsoft.AspNetCore.Mvc;

namespace DogsService.Tests
{
    public class HealthCheckControllerTests
    {
        [Fact]
        public void Ping_ReturnsOkResult()
        {
            // Arrange
            var controller = new HealthCheckController();

            // Act
            var result = controller.Ping();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

    }
}

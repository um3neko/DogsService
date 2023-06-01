using CodeBridge.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

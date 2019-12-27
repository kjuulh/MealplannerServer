using System.Net.Http;
using System.Threading.Tasks;
using MealplannerServer;
using MealplannerServer.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MealplannerIntegrationtests.Controllers
{

    class MessageObj
    {
        public string Message { get; set; }
    }

    public class HealthControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {

        private readonly WebApplicationFactory<Startup> _factory;

        public HealthControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_HealthPing_ReturnsPong()
        {

            // Arrange
            var client = _factory.CreateClient();
            // Act
            var response = await client.GetAsync("/api/health/ping");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var content = await response.Content.ReadAsAsync<MessageObj>();

            Assert.Equal("pong!", content.Message);
        }

    }
}
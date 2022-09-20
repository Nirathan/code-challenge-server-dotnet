using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using server.Models;
using server.Services;
using Xunit;

namespace server.Tests
{
    public class TemperatureControllerIntegrationTest
    {
        private readonly ISensorService sensorService;
        private readonly WebApplicationFactory<Program> application;

        public TemperatureControllerIntegrationTest()
        {
            sensorService = Mock.Of<ISensorService>();

            application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(service =>
                    {
                        service.AddSingleton<ISensorService>(sensorService);
                    });
                });
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("3")]
        public async Task TemperatureGetEndpoint_GivenValidSensorId_ReturnsSensor(string sensorId)
        {
            Mock.Get(sensorService)
                .Setup(s => s.Get(It.IsAny<string>()))
                .ReturnsAsync(new Sensor { Id = sensorId, Temperature = 12 });

            var client = application.CreateClient();

            var response = await client.GetAsync($"/Temperature/{sensorId}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<Sensor>();

            result.Should().NotBeNull();
            result.Id.Should().BeEquivalentTo(sensorId);
        }
    }
}


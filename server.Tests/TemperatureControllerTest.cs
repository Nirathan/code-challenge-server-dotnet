using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using server.Controllers;
using server.Models;
using server.Services;
using Xunit;

namespace server.Tests
{
    public class TemperatureControllerTest
    {
        private readonly ISensorService sensorService;
        private readonly TemperatureController subject;

        public TemperatureControllerTest()
        {
            sensorService = Mock.Of<ISensorService>();
            subject = new TemperatureController(sensorService);
        }

        [Fact]
        public async Task Get_GivenSensorId_ReturnsOK()
        {
            var mockId = "mockeId";
            var sensor = new Sensor { Id = mockId, Temperature = 1 };

            Mock.Get(sensorService)
                .Setup(s => s.Get(It.IsAny<string>()))
                .ReturnsAsync(sensor)
                .Verifiable();

            var result = await subject.Get(mockId);

            Mock.Get(sensorService).Verify(mock => mock.Get(It.IsAny<string>()), Times.Once);
            result.Should().BeEquivalentTo(new OkObjectResult(sensor));
        }
    }
}


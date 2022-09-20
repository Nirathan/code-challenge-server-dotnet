using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using server.Configurations;
using server.Models;
using server.Services;
using Xunit;

namespace server.Tests
{
    public class SensorServiceTest
    {
        private readonly HttpMessageHandler httpMessageHandler;
        private readonly IOptions<SensorServiceConfig> configuration;
        private readonly SensorService subject;

        public SensorServiceTest()
        {
            var logger = Mock.Of<ILogger<SensorService>>();
            httpMessageHandler = Mock.Of<HttpMessageHandler>();
            configuration = Options.Create(new SensorServiceConfig { Url = "http://mockUrl" });

            var httpClient = new HttpClient(httpMessageHandler);
            subject = new SensorService(httpClient, configuration, logger);
        }

        [Fact]
        public async Task Get_GivenSensorId_ReturnsSensor()
        {
            var sensorId = "mockId";
            var mockSensor = new Sensor { Id = sensorId, Temperature = 1 };

            var mockSensorResponse = new HttpResponseMessage(HttpStatusCode.OK);
            mockSensorResponse.Content = JsonContent.Create(mockSensor);

            Mock.Get(httpMessageHandler)
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockSensorResponse);

            var result = await subject.Get(sensorId);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new Sensor { Id = sensorId, Temperature = 1 });
        }

        [Fact]
        public async Task Get_WhenServiceHaveIssue_ThrowExceptions()
        {
            var sensorId = "mockId";

            Mock.Get(httpMessageHandler)
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(async () => await subject.Get(sensorId));
        }
    }
}


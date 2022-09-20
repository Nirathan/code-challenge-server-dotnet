using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using server.Configurations;
using server.Models;

namespace server.Services
{
    public class SensorService : ISensorService
    {
        private readonly HttpClient httpClient;
        private readonly SensorServiceConfig config;
        private readonly ILogger<SensorService> logger;

        public SensorService(
            HttpClient httpClient,
            IOptions<SensorServiceConfig> config,
            ILogger<SensorService> logger)
        {
            this.httpClient = httpClient;
            this.config = config.Value;
            this.logger = logger;
        }

        public async Task<Sensor> Get(string sensorId)
        {
            try
            {
                var response = await httpClient.GetAsync($"{config.Url}{sensorId}");
                var jsonString = await response.Content.ReadAsStringAsync();
                var sensorData = JsonSerializer.Deserialize<Sensor>(jsonString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                logger.LogInformation("SensorId {0}, Temperature: {1}", sensorData.Id, sensorData.Temperature);
                return sensorData;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "SensorId {0}", sensorId);
                throw;
            }
        }
    }
}


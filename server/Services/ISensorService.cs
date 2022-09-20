using System;
using System.Threading.Tasks;
using server.Models;

namespace server.Services
{
    public interface ISensorService
    {
        Task<Sensor> Get(string sensorId);
    }
}


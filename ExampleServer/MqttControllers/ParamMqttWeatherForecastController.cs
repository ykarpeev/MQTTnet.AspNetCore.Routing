using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace Example.MqttControllers
{
    [MqttController]
    [MqttRoute("devices/{devname}/[controller]")]
    public class ParamMqttWeatherForecastController : MqttBaseController
    {
        private readonly ILogger<ParamMqttWeatherForecastController> _logger;

        // Controllers have full support for dependency injection just like AspNetCore controllers
        public ParamMqttWeatherForecastController(ILogger<ParamMqttWeatherForecastController> logger)
        {
            _logger = logger;
        }

      
        public string devname { get; set; }
        [MqttRoute()]
        public Task WeatherReport2()
        {
            _logger.LogInformation($"WeatherReport2");
            return Ok();
        }

        // Supports template routing with typed constraints
        [MqttRoute("{zipCode}/temperature")]
        public Task WeatherReport(int zipCode)
        {
            // We have access to the MqttContext
            if (zipCode != 90210) { MqttContext.CloseConnection = true; }

            // We have access to the raw message
            if (int.TryParse(Message.ConvertPayloadToString(), out int temperature))
            {
                _logger.LogInformation($"It's {temperature} degrees in Hollywood");
                // Example validation
                if (temperature <= 0 || temperature >= 130)
                {
                    return BadMessage();
                }
                else
                {
                    return Ok();
                }
            }
            return BadMessage();
        }
    }
}
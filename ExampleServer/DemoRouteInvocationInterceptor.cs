using ExampleServer.MqttControllers;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.AspNetCore.Routing.Routing;
using System;
using System.Threading.Tasks;

namespace ExampleServer
{
    public class DemoRouteInvocationInterceptor : IRouteInvocationInterceptor
    {
        private readonly ILogger _logger;

        public DemoRouteInvocationInterceptor(ILogger<DemoRouteInvocationInterceptor> logger)
        {
            _logger = logger;
        }
        public Task RouteExecuted(object o, Exception ex)
        {
            _logger.LogInformation($" {ex.Message}");
            return Task.CompletedTask;
        }

        public Task<object> RouteExecuting(string clientId, MqttApplicationMessage applicationMessage)
        {
            object obj = new { clientId, applicationMessage.Topic };
            _logger.LogInformation($"{clientId},{applicationMessage.Topic}");
            return Task.FromResult(obj);
        }
    }
}

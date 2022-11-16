using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MQTTnet.AspNetCore.Routing;
using MQTTnet.AspNetCore.Routing.Attributes;

namespace ExampleServer.MqttControllers;

[MqttController]
public class ViewModelController: MqttBaseController
{
    private ILogger<ViewModelController> Logger { get; }
    
    public ViewModelController(ILogger<ViewModelController> logger)
    {
        Logger = logger;
    }

    [MqttRoute("viewmodel/{sender}")]
    public Task DeserializeViewModel(string sender, [FromPayload] SamplePayload payload)
    {
        Logger.LogInformation("{Sender} says {Message}", sender, payload.Message);
        return Accepted();
    }
}

public class SamplePayload
{
    public string Message { get; set; }
}
using System.Text.Json;

namespace MQTTnet.AspNetCore.Routing;

public class MqttDefaultJsonOptions
{
    public JsonSerializerOptions SerializerOptions { get; }

    public MqttDefaultJsonOptions(JsonSerializerOptions options)
    {
        SerializerOptions = options;
    }
}
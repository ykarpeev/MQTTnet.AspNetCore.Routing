using MQTTnet;
using System;

namespace ExampleClient
{
    internal class Program
    {
        private static async System.Threading.Tasks.Task Main(string[] args)
        {
            var rnd = new Random();
            var options = new MqttClientOptionsBuilder ()
                    .WithClientId($"Client{rnd.Next(0, 1000)}")
                    .WithWebSocketServer( cfg=> cfg.WithUri("ws://localhost:5000/mqtt"))
                    .Build();

            var mqttClient = new MqttClientFactory().CreateMqttClient();
            mqttClient.ConnectedAsync += (e) =>
            {
                Console.WriteLine($"Connected : {e.ConnectResult.ResultCode}");
                return System.Threading.Tasks.Task.CompletedTask;
            };
            mqttClient.DisconnectedAsync += (e) =>
            {
                Console.WriteLine($"Disconnected : {e.Exception}");
                return System.Threading.Tasks.Task.CompletedTask;
            };
            mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                Console.WriteLine($"Message from {e.ClientId}: {e.ApplicationMessage.Payload.Length } bytes.");
                return System.Threading.Tasks.Task.CompletedTask;
            };
            await mqttClient.ConnectAsync(options);
            await mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("MqttWeatherForecast/90210/temperature").Build());
            await mqttClient.PublishAsync(new  MqttApplicationMessageBuilder().WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce).WithPayload(BitConverter.GetBytes(98.6d)).WithTopic("MqttWeatherForecast/90210/temperature").Build());
            await mqttClient.PublishAsync(new MqttApplicationMessageBuilder().WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce).WithPayload(BitConverter.GetBytes(100d)).WithTopic("asdfsdfsadfasdf").Build());
            Console.ReadLine();
        }
    }
}
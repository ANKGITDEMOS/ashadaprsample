namespace Simulation.Proxies;

public class MqttAshaService : IAshaService
{
    private IMqttClient _client;

    private MqttAshaService(IMqttClient mqttClient)
    {
        _client = mqttClient;
    }

    public static async Task<MqttAshaService> CreateAsync()
    {
        var mqttHost = Environment.GetEnvironmentVariable("MQTT_HOST") ?? "localhost";
        var factory = new MqttFactory();
        var client = factory.CreateMqttClient();
        var mqttOptions = new MqttClientOptionsBuilder()
            .WithTcpServer(mqttHost, 1883)
            .WithClientId("Punjab")
            .Build();
        await client.ConnectAsync(mqttOptions, CancellationToken.None);
        return new MqttAshaService(client);
    }

    public async Task RegisterCitizenAsync(CitizenRequest citizenrequest)
    {
        var eventJson = JsonSerializer.Serialize(citizenrequest);
        var message = new MqttApplicationMessageBuilder()
            .WithTopic("asha/registercitizen")
            .WithPayload(Encoding.UTF8.GetBytes(eventJson))
            .WithAtMostOnceQoS()
            .Build();
        await _client.PublishAsync(message, CancellationToken.None);
    }

}

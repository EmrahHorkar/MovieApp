using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using MovieApp.Models;
using MovieApp.Data;

public class MessageProducerService
{
    private readonly IConnection _connection;
    private readonly string _queueName;
    
    public MessageProducerService(IConfiguration configuration)
    {
        var factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQ:Hostname"],
            Port = int.Parse(configuration["RabbitMQ:Port"]),
            UserName = configuration["RabbitMQ:Username"],
            Password = configuration["RabbitMQ:Password"]
        };
       
        _connection = factory.CreateConnection();
        _queueName = configuration["RabbitMQ:QueueName"];
    }

    public void SendMessage(UserMessage message)
    {
        using (var channel = _connection.CreateModel())
        {
            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var messageJson = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(messageJson);

            channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
        }
        //_context.UserMessages.Add(message);
        //_context.SaveChanges();
    }
}

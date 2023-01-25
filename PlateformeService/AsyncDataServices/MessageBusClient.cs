using Microsoft.Extensions.Configuration;
using PlateformeService.Dtos;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace PlateformeService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;

            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += _connection_ConnectionShutdown;

                Console.WriteLine("--> Connected to message bus");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not connect to message bus ::");
                Console.WriteLine(ex.ToString());
            }

        }

        private void _connection_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("Connction terminée");

        }

        public void PublishNewPlatform(PlateformPubDto dto)
        {
            if (_connection.IsOpen)
            {
                var message = JsonSerializer.Serialize(dto);
                SendMessage(message);

                Console.WriteLine("Connection is open, sending message...");
            }
            else
            {
                Console.WriteLine("Connection is closed, can not send message");
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish("trigger", "", null, body);


            Console.WriteLine($"we have sent {message}");
        }

        public void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
    }
}

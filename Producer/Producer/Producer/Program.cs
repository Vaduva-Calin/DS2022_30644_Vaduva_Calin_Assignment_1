using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ.Producer
{
    static class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqps://bjdnpbwl:pWmj5aZo4ralEfE1__JBqVGQwsWgmJKn@goose.rmq2.cloudamqp.com/bjdnpbwl")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("BrokerQueue",
                durable: false,
                exclusive: false,
                autoDelete: true,
                null);

            int id;
            int delay;

            using (var reader = new StreamReader(@"configuration.txt"))
            {
                id = Int32.Parse(reader.ReadLine().Split(' ')[1]);
                delay = Int32.Parse(reader.ReadLine().Split(' ')[1]);
            }

            using (var reader = new StreamReader(@"sensor.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var message = new
                    {
                        timeStamp = DateTimeOffset.Now.DateTime,
                        device_id = id,
                        measurement_value = reader.ReadLine(),
                    };
                    var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                    var exchangeName = "";
                    var routingKey = "BrokerQueue";
                    channel.BasicPublish(exchangeName, routingKey, null, data);
                    Console.WriteLine("this is the message: " + message);
                    Thread.Sleep(delay * 1000);
                }
            }
        }
    }
}
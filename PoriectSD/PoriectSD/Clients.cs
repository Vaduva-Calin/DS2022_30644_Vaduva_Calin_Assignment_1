using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using PoriectSD;
using RabbitMQ.Client;
using System;
using System.Security.Policy;
namespace WebAPI.Extensions;

public static class Clients
{
   public static WebApplication Client(this WebApplication app)
    {
        var factory = new ConnectionFactory
        {
            Port = 5672,
            Uri = new Uri("amqps://bjdnpbwl:pWmj5aZo4ralEfE1__JBqVGQwsWgmJKn@goose.rmq2.cloudamqp.com/bjdnpbwl")
        };

        IConnection connection = factory.CreateConnection();
        IModel channel = connection.CreateModel();

        channel.QueueDeclare("BrokerQueue",
        durable: false,
        exclusive: false,
        autoDelete: true,
        null);
        var provider = app.Services;
     
        app.Use(async (context, next) =>
        {
            var hub = context.RequestServices.GetRequiredService<IHubContext<ClientHub>>();
            var consumer = new Consumer(channel, provider, hub);
            channel.BasicConsume("BrokerQueue", false, consumer);
            if (next != null)
            {
                await next.Invoke();
            }
        });

        return app;
    }
}

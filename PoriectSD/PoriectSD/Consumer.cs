using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PoriectSD.Database;
using PoriectSD.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace PoriectSD
{
    public class Consumer : DefaultBasicConsumer
    {
        private static IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHubContext<ClientHub> _hub;

        public Consumer(IModel channel, IServiceProvider serviceProvider, IHubContext<ClientHub> hub)
        {
            _serviceProvider = serviceProvider;
            _channel = channel;
            _hub = hub;
        }

        public override async void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            var message = Encoding.UTF8.GetString(body.ToArray());
            Console.WriteLine("Citire senzor : " + message);
            EnergyQueue data = JsonConvert.DeserializeObject<EnergyQueue>(message);

            if (data is null)
            {
                _channel.BasicAck(deliveryTag, false);
                Console.WriteLine("E null");
                return;
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SDDbContext>();
                var deviceInDb = context.Devices.FirstOrDefault(x => x.Id == data.Device_id);

                if (deviceInDb != null)
                {
                    var consumption = 0.0;
                    context.Energies.Add(new Energy { DeviceId = data.Device_id, EnergyConsumption = data.Measurement_value, Timestamp = data.Timestamp });
                    var device = await context.Devices.FirstOrDefaultAsync(x => x.Id == data.Device_id);
                    var consumptions = await context.Energies.Where(x => x.Timestamp.Hour == data.Timestamp.Hour).ToListAsync();
                    var limit = device.MaxConsumption;

                    foreach (var cons in consumptions)
                    {
                        consumption += cons.EnergyConsumption;
                    }

                    if (consumption >= limit)
                    {
                        await _hub.Clients.All.SendAsync("notifyClient", device.Address);
                    }

                    context.SaveChanges();
                    await _hub.Clients.All.SendAsync("refreshChart");
                }
            }
        }
    }
    public class ClientHub : Hub
    {
        public async Task Send()
        {
            await Clients.All.SendAsync("ReceiveNotification");
        }
    }
}


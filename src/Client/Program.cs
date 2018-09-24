using System;
using System.Text;
using System.Threading.Tasks;
using Core;
using STAN.Client;

namespace Client
{
    class Program
    {   
        static async Task Main(string[] args)
        {
            var opts = StanOptions.GetDefaultOptions();
            opts.NatsURL = "nats://localhost:4223";
            
            using (var c = new StanConnectionFactory().CreateConnection(NatsConfig.ClusterId, NatsConfig.ClientClientId, opts))
            {
                Console.WriteLine($"Connected With Id: {c.NATSConnection.ConnectedId}");
            
                Console.Read();
                
                byte[] payload = Encoding.UTF8.GetBytes("hello");
                await c.PublishAsync(NatsConfig.Subject, payload);
            }

            Console.Read();
        }
    }
}
using System;
using System.Text;
using Core;
using STAN.Client;

namespace Server
{
    class Program
    {   
        static void Main(string[] args)
        {
            var subscriptionOpts = StanSubscriptionOptions.GetDefaultOptions();
            var opts = StanOptions.GetDefaultOptions();
            opts.NatsURL = "nats://localhost:4223";

            void MsgHandler(object sender, StanMsgHandlerArgs props)
            {
                var message = Encoding.UTF8.GetString(props.Message.Data);
                Console.WriteLine($"Received seq # {props.Message.Sequence}: {message}");
            }

            using (var c = new StanConnectionFactory().CreateConnection(NatsConfig.ClusterId, NatsConfig.ServerClientId, opts))
            {
                Console.WriteLine($"Connected With Id: {c.NATSConnection.ConnectedId}");
            
                using (var s = c.Subscribe(NatsConfig.Subject, subscriptionOpts, MsgHandler))
                {
                    Console.Read();
                }
            }

            Console.Read();
        }
    }
}
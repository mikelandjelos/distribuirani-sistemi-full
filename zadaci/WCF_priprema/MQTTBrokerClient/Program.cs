using MQTTBrokerClient.MQTTBrokerServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MQTTBrokerClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            InstanceContext instanceContext = new InstanceContext(new PublishCallback());
            MQTTBrokerServiceClient client = new MQTTBrokerServiceClient(instanceContext);

            string topic1 = "my-topic-1";
            string topic2 = "my-topic-2";
            string topic3 = "my-topic-3";

            for (int i = 0; i < 10; ++i)
            {

                await client.PublishAsync(topic3, "Never seen");

                await client.SubscribeAsync(topic1);

                await client.PublishAsync(topic1, "First message");     // should show
                await client.PublishAsync(topic2, "Second message");    // shouldn't show

                await client.SubscribeAsync(topic2);

                await client.PublishAsync(topic2, "Third message");     // should show

                await client.UnsubscribeAsync(topic1);

                await client.PublishAsync(topic1, "Fourth message");    // shouldn't show

                await client.UnsubscribeAsync(topic2);

                await client.PublishAsync(topic2, "Fifth message");     // shouldn't show

                await client.SubscribeAsync(topic3);

            }

            for (int i = 10; i > 0; --i)
            {
                Console.WriteLine($"{i}...");
                Thread.Sleep(1000);
            }

            await client.UnsubscribeAsync(topic3);
        }
    }

    public class PublishCallback : IMQTTBrokerServiceCallback
    {
        public void OnPublish(string topic, string message)
        {
            Console.WriteLine($"Published[{topic}]: '{message}'");
        }
    }
}

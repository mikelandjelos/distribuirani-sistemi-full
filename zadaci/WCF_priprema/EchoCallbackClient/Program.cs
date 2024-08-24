using EchoCallbackClient.EchoCallbackServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EchoCallbackClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            EchoServiceClient proxy = new EchoServiceClient(new InstanceContext(new EchoCallback()));

            await proxy.EchoAsync($"Hello from {Guid.NewGuid()}");

            Console.WriteLine("Press a key to continue...");
            Console.ReadKey();
        }

        public class EchoCallback : IEchoServiceCallback
        {
            public void OnEcho(string message)
            {
                Console.WriteLine(message);
            }
        }
    }
}

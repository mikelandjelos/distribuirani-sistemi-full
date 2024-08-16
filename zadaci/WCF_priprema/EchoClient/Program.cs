using EchoClient.EchoServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            EchoServiceClient echoServiceClient = new EchoServiceClient();
            bool clientActive = true;
            while (clientActive)
            {
                Console.WriteLine(
                    "What do you want to do? Choose option:\n" +
                    "\t[1] - Send simple string message.\n" +
                    "\t[2] - Send composite type message.\n" +
                    "\t[3] - Exit.");

                char option = Console.ReadKey().KeyChar;

                Console.WriteLine();

                switch (option)
                {
                    case '1':
                        Console.Write("Enter a message: ");
                        string message = Console.ReadLine();
                        string echo = await echoServiceClient.EchoAsync(message);
                        Console.WriteLine("You received an echo: `{0}`", echo);
                        break;
                    case '2':
                        Console.Write("Enter a boolean value (true/false): ");

                        bool compositeMemberBool;
                        if (!bool.TryParse(Console.ReadLine(), out compositeMemberBool))
                        {
                            Console.WriteLine("Invalid boolean!");
                            break;
                        }

                        Console.Write("Enter a string value: ");
                        string compositeMemberString = Console.ReadLine();

                        CompositeType compositeData = new CompositeType
                        {
                            BoolValue = compositeMemberBool,
                            StringValue = compositeMemberString
                        };

                        CompositeType compositeDataEcho = echoServiceClient.GetCompositeData(compositeData);

                        Console.WriteLine($"You received a composite echo: {{ BoolValue = {compositeData.BoolValue}, StringValue = `{compositeData.StringValue}` }}");
                        break;
                    case '3':
                        clientActive = false;
                        break;
                    default:
                        Console.WriteLine("Unknown option! Try again.");
                        break;
                }
            }
        }
    }
}

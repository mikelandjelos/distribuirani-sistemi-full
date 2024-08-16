
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using SimpleMessagingClient;

using var channel = GrpcChannel.ForAddress("http://localhost:5009");
var simpleMessagingClientStub = new SimpleMessaging.SimpleMessagingClient(channel);

for (; ; )
{
    Console.WriteLine(
        "What would you like to do?\n" +
        "\t- Send message [1]\n" +
        "\t- Delete message [2]\n" +
        "\t- List all messages [3]");

    char option = Console.ReadKey().KeyChar;
    Console.WriteLine();

    switch (option)
    {
        case '1':
            Console.Write("Enter message contents: ");
            string? contents = Console.ReadLine();
            var generatedId = await simpleMessagingClientStub.SendMessageAsync(new StringValue { Value = contents });
            Console.WriteLine($"Message sent successfully; generated ID - {generatedId}");
            break;
        case '2':
            Console.Write("Enter id of the message you want to delete: ");
            string? idToDelete = Console.ReadLine();
            bool deleteSuccessful = (await simpleMessagingClientStub.DeleteMessageAsync(new StringValue { Value = idToDelete })).Value;
            if (deleteSuccessful)
                Console.WriteLine("Successfully deleted message!");
            else
                Console.WriteLine($"Message with ID `{idToDelete}` doesn't exist!");
            break;
        case '3':
            var messageStream = simpleMessagingClientStub.ListMessages(new Empty());
            while (await messageStream.ResponseStream.MoveNext(CancellationToken.None))
                Console.WriteLine($"Message [{messageStream.ResponseStream.Current.Id}]: `{messageStream.ResponseStream.Current.Contents}`");
            break;
        default:
            Console.WriteLine("Unknown option! Try again!");
            break;
    }
}

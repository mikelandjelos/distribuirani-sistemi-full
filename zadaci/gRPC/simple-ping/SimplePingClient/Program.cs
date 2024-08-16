using Grpc.Net.Client;
using SimplePingClient;

using var channel = GrpcChannel.ForAddress("http://localhost:5070");
var simplePingClientStub = new SimplePing.SimplePingClient(channel);

var pongResponse = await simplePingClientStub.PingAsync(new Google.Protobuf.WellKnownTypes.Empty());

Console.WriteLine($"Service pinged successfully!");

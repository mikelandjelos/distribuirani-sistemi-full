using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using SimplePingServer;

namespace SimplePingServer.Services;

public class SimplePingService : SimplePing.SimplePingBase
{
    private readonly ILogger<SimplePingService> _logger;
    public SimplePingService(ILogger<SimplePingService> logger)
    {
        _logger = logger;
    }

    public override Task<StringValue> Ping(Empty request, ServerCallContext context)
    {
        _logger.LogInformation($"Pinged by {context.Peer}");
        return Task.FromResult(new StringValue { Value = "Pong!" });
    }
}

using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using SimpleMessagingServer;

namespace SimpleMessagingServer.Services;

public class SimpleMessagingService : SimpleMessaging.SimpleMessagingBase
{
    private readonly ILogger<SimpleMessagingService> _logger;
    private readonly object _lockObj = new();
    private static Dictionary<string, string> messages = new();

    public SimpleMessagingService(ILogger<SimpleMessagingService> logger)
    {
        _logger = logger;
    }

    public override Task<StringValue> SendMessage(StringValue request, ServerCallContext context)
    {
        var generatedId = Guid.NewGuid().ToString();
        lock (_lockObj)
            messages.Add(generatedId, request.Value);
        _logger.LogInformation($"Written message with ID {generatedId}!");
        return Task.FromResult(new StringValue { Value = generatedId });
    }

    public override Task<BoolValue> DeleteMessage(StringValue request, ServerCallContext context)
    {
        var messageFound = messages.ContainsKey(request.Value);
        _logger.LogInformation($"Message with ID `{request.Value}` {(messageFound ? string.Empty : "not")} found!");
        if (messageFound)
            lock (_lockObj)
                messages.Remove(request.Value);
        return Task.FromResult(new BoolValue { Value = messageFound });
    }

    public override async Task ListMessages(Empty request, IServerStreamWriter<Message> responseStream, ServerCallContext context)
    {
        var messagesToSend = messages.Select(pair => new Message { Id = pair.Key, Contents = pair.Value });
        foreach (var message in messagesToSend)
            await responseStream.WriteAsync(message);
    }
}

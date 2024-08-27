using System.Collections.Concurrent;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Messaging.Services;

public class GreeterService : Messaging.MessagingBase
{
    private readonly ILogger<GreeterService> _logger;
    private static ConcurrentDictionary<Guid, Message> _messages = new();

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<StringValue> SendMessage(Message request, ServerCallContext context)
    {
        var generatedGuid = Guid.NewGuid();
        request.Uuid = generatedGuid.ToString();
        var success = _messages.TryAdd(generatedGuid, request);

        if (!success)
            Task.FromResult(new StringValue { Value = string.Empty });

        return Task.FromResult(new StringValue { Value = generatedGuid.ToString() });
    }

    public override Task<Message> DeleteMessage(StringValue request, ServerCallContext context)
    {
        Message? deletedMessage;

        if (!_messages.Remove(Guid.Parse(request.Value), out deletedMessage))
            throw new OperationCanceledException($"Message with given ID not found (`{request.Value}`)");

        return Task.FromResult(deletedMessage);
    }

    public override async Task ListMessages(Empty request, IServerStreamWriter<Message> responseStream, ServerCallContext context)
    {
        foreach (var message in _messages.Values)
            await responseStream.WriteAsync(message);
    }
}

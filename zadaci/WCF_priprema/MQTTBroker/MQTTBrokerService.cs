using System.Collections.Concurrent;
using System.Linq;
using System.ServiceModel;
using Topic = System.String;

namespace MQTTBroker
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class MQTTBrokerService : IMQTTBrokerService
    {
        static ConcurrentDictionary<Topic, ConcurrentBag<IPublishCallback>> topicCatalog =
            new ConcurrentDictionary<Topic, ConcurrentBag<IPublishCallback>>();

        IPublishCallback callback = null;

        public void Subscribe(string topic)
        {
            callback = OperationContext.Current.GetCallbackChannel<IPublishCallback>();

            var subscribers = topicCatalog.GetOrAdd(topic, _ => new ConcurrentBag<IPublishCallback>());
            subscribers.Add(callback);
        }

        public void Unsubscribe(string topic)
        {
            if (topicCatalog.TryGetValue(topic, out var subscribers))
            {
                subscribers = new ConcurrentBag<IPublishCallback>(
                    subscribers.Where(cb => cb != callback));

                if (subscribers.IsEmpty)
                    topicCatalog.TryRemove(topic, out _);
                else
                    topicCatalog[topic] = subscribers;
            }
        }

        public void Publish(string topic, string message)
        {
            if (topicCatalog.TryGetValue(topic, out var subscribers))
            {
                foreach (var subscriber in subscribers)
                {
                    subscriber?.OnPublish(topic, message);
                }
            }
        }
    }
}

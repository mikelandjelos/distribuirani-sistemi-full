using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MQTTBroker
{
    /// <summary>
    /// Message broker servis - nije bas najbolji primer primene WCF-a, ali je dobar za razumevanje.
    /// </summary>
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IPublishCallback))]
    public interface IMQTTBrokerService
    {
        [OperationContract(IsOneWay = true, IsInitiating = true)]
        void Subscribe(string topic);
        [OperationContract(IsOneWay = true)]
        void Unsubscribe(string topic);
        [OperationContract(IsOneWay = true, IsInitiating = true)]
        void Publish(string topic, string message);
    }
}

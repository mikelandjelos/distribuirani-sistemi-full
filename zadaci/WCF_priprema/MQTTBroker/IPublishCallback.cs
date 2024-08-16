using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MQTTBroker
{
    public interface IPublishCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnPublish(string topic, string message);
    }
}

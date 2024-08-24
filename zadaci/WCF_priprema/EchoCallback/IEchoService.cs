using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EchoCallback
{
    [ServiceContract(CallbackContract = typeof(IEchoCallback), SessionMode = SessionMode.Required)]
    public interface IEchoService
    {
        [OperationContract(IsOneWay = true)]
        void Echo(string message);
    }
}

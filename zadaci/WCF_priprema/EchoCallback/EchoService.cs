using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EchoCallback
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)] // Ili `InstanceContextMode.PerSession`, mada pazi koji kad koristis
    public class EchoService : IEchoService
    {
        private IEchoCallback Callback = OperationContext.Current.GetCallbackChannel<IEchoCallback>();
        public void Echo(string message)
        {
            Callback.OnEcho($"Echoing: `{message}`");
        }
    }
}

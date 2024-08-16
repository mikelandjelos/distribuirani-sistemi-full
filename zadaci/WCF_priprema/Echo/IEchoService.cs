using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Echo
{
    /// <summary>
    /// Prvi kreirani servis - kratak primer.
    /// </summary>
    [ServiceContract]
    public interface IEchoService
    {
        [OperationContract]
        string Echo(string value);

        [OperationContract]
        CompositeType GetCompositeData(CompositeType composite);
    }

    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}

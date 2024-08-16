using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Echo
{
    public class EchoService : IEchoService
    {
        public string Echo(string value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetCompositeData(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}

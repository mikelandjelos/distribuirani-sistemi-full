using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    internal interface IChatServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void RegistracijaEvent(string povratneInformacije);
        [OperationContract(IsOneWay = true)]
        void PrijavaOdjavaEvent(string povratneInformacije);
        [OperationContract(IsOneWay = true)]
        void PoslataPorukaEvent(Poruka poruka);
        [OperationContract(IsOneWay = true)]
        void ZatrazenaIstorijaPrimljenihPorukaEvent(List<Poruka> istorijaPoruka);
    }
}

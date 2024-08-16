using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Chat
{
    [ServiceContract(CallbackContract = typeof(IChatServiceCallback), SessionMode = SessionMode.Required)]
    public interface IChatService
    {
        [OperationContract(IsOneWay = true)]
        void Registracija(string nadimak, string sifra);
        [OperationContract(IsOneWay = true)]
        void PrijavaOdjava(string nadimak, string sifra);
        [OperationContract(IsOneWay = true)]
        void Posalji(Poruka poruka);
        [OperationContract(IsOneWay = true)]
        void IstorijaPrimljenihPoruka(string nadimak, DateTime od, DateTime @do);
    }

    [DataContract]
    public class Poruka
    {
        [DataMember]
        public string Posiljalac { get; set; }
        [DataMember]
        public string Primalac { get; set; }
        [DataMember]
        public string Sadrzaj { get; set; }
        [DataMember]
        public DateTime VremeSlanja { get; set; }
    }
}

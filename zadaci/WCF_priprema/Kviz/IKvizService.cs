using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Kviz
{
    /// <summary>
    /// Kviz servis - igranje sa sesijama.
    /// </summary>
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IKvizService
    {
        [OperationContract]
        void DodajPitanje(Pitanje pitanje);

        [OperationContract]
        List<Pitanje> VratiPitanja();

        [OperationContract]
        void IzmeniPitanje(int redniBroj, Pitanje novoPitanje);

        [OperationContract]
        double EvaluirajRezultat(List<int> datiOdgovori);
    }

    [DataContract]
    public class Pitanje
    {
        [DataMember]
        public string TekstPitanja { get; set; }

        [DataMember]
        public List<string> PonudjeniOdgovori { get; set; }

        [DataMember]
        public int RedniBrojTacnogOdgovora { get; set; }
    }
}

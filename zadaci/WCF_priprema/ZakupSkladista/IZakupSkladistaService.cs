using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace ZakupSkladista
{
    /// <summary>
    /// Jun 2024.
    /// </summary>
    [ServiceContract]
    public interface IZakupSkladistaService
    {
        [OperationContract]
        Zakup ZakupiSkladiste(Vlasnik vlasnik, Skladiste skladiste, DateTime pocetakZakupa, DateTime krajZakupa);
        [OperationContract]
        List<Skladiste> VratiAktivnaSkladista(string jmbgVlasnika);
        [OperationContract]
        List<Vlasnik> VratiVlasnikeAktivnihSkladista();
        [OperationContract]
        Dictionary<Skladiste, List<Zakup>> VratiSkladistaSaIstorijomZakupa();
    }

    [DataContract]
    public class Vlasnik
    {
        [DataMember]
        public string Ime { get; set; }
        [DataMember]
        public string Prezime { get; set; }
        [DataMember]
        public string Jmbg { get; set; }
    }

    [DataContract]
    public class Skladiste
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public double Cena { get; set; }
    }

    [DataContract]
    public class Zakup
    {
        [DataMember]
        public string JmbgVlasnika { get; set; }
        [DataMember]
        public string IdSkladista { get; set; }
        [DataMember]
        public DateTime PocetakZakupa { get; set; }
        [DataMember]
        public DateTime KrajZakupa { get; set; }
    }
}

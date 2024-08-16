using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RegistracijaVozila
{
    /// <summary>
    /// Jun 2 2024.
    /// </summary>
    [ServiceContract]
    public interface IRegistracijaVozilaService
    {
        [OperationContract]
        Registracija RegistrujVozilo(Vlasnik vlasnik, Vozilo vozilo, DateTime krajRegistracije);
        [OperationContract]
        List<Vozilo> VratiVozila(string jmbgVlasnika);
        [OperationContract]
        List<Registracija> VratiIstorijuRegistracija(Vozilo vozilo);
        [OperationContract]
        Dictionary<Vozilo, List<Registracija>> VratiVozilaSaRegistracijama();
    }

    [DataContract]
    public class Vlasnik
    {
        [DataMember]
        public string Jmbg { get; set; }
        [DataMember]
        public string Ime { get; set; }
        [DataMember]
        public string Prezime { get; set; }
    }

    [DataContract]
    public class Vozilo
    {
        [DataMember]
        public string Marka { get; set; }
        [DataMember]
        public string Model { get; set; }
        [DataMember]
        public string Boja { get; set; }

        public override string ToString()
        {
            return $"{Marka}|{Model}|{Boja}";
        }
    }

    [DataContract]
    public class Registracija
    {
        [DataMember]
        public string JmbgVlasnika { get; set; }
        [DataMember]
        public string IdVozila { get; set; }
        [DataMember]
        public DateTime PocetakRegistracije { get; set; }
        [DataMember]
        public DateTime KrajRegistracije { get; set; }
    }
}

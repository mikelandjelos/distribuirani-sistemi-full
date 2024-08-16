using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MatricnaIzracunavanja
{
    [ServiceContract]
    public interface IMatricnaIzracunavanjaService
    {
        [OperationContract]
        Rezultat Postavi(Matrica matrica);
        [OperationContract]
        Rezultat PreuzmiMatricu();
        [OperationContract]
        Rezultat Sabiranje(Matrica matrica);
        [OperationContract]
        Rezultat MnozenjeMatricom(Matrica matrica);
        [OperationContract]
        Rezultat MnozenjeSkalarom(int matrica);
        [OperationContract]
        Rezultat Transponuj();
    }

    [DataContract]
    public class Matrica
    {
        [DataMember]
        public int[] Elementi { get; set; }
        [DataMember]
        public int BrojVrsta { get; set; }
        [DataMember]
        public int BrojKolona { get; set; }
    }

    [DataContract]
    public class Rezultat
    {
        [DataMember]
        public Matrica Matrica { get; set; }
        [DataMember]
        public bool Uspeh { get; set; }
        [DataMember]
        public string Poruka { get; set; } // Informacije o uspehu/gresci
    }
}

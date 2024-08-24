using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RegistracijaParcela
{
    [ServiceContract]
    public interface IRegistracijaParcelaService
    {
        [OperationContract]
        string EchoTest(string message);
        [OperationContract]
        void RegistrujParcelu(Parcela parcela, string imeVlasnika);
        [OperationContract]
        List<Tuple<Parcela, decimal>> VratiParceleSaPovrsinom(string imeVlasnika);
        [OperationContract]
        List<Parcela> VratiVeceParcele(decimal povrsinaUArima);
        [OperationContract]
        List<Parcela> VratiSveParcele();
    }

    [DataContract]
    public class Parcela
    {
        [DataMember]
        public List<Tuple<decimal, decimal>> Koordinate { get; set; }

        [DataMember]
        public string ImeVlasnika { get; set; }

        // Povrsina bilo kog poligona zadatog torkom njegovih kartezijanskih koordinata:
        // https://en.wikipedia.org/wiki/Shoelace_formula
        public decimal IzracunajPovrsinuMetriKvadratni()
        {
            int n = Koordinate.Count;
            decimal area = 0;

            for (int i = 0; i < n - 1; i++)
            {
                area += Koordinate[i].Item1 * Koordinate[i + 1].Item2;
                area -= Koordinate[i].Item2 * Koordinate[i + 1].Item1;
            }

            area += Koordinate[n - 1].Item1 * Koordinate[0].Item2;
            area -= Koordinate[n - 1].Item2 * Koordinate[0].Item1;

            return Math.Abs(area / 2);
        }
    }
}

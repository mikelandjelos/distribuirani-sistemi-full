using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RegistracijaParcela
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RegistracijaParcelaService : IRegistracijaParcelaService
    {
        private Dictionary<string, List<Parcela>> _registrovaneParcele = new Dictionary<string, List<Parcela>>();

        public string EchoTest(string message)
        {
            return $"Echoing: `{message}`";
        }

        public void RegistrujParcelu(Parcela parcela, string imeVlasnika)
        {
            if (!_registrovaneParcele.ContainsKey(imeVlasnika))
                _registrovaneParcele.Add(imeVlasnika, new List<Parcela>());

            _registrovaneParcele[imeVlasnika].Add(parcela);
        }

        public List<Tuple<Parcela, decimal>> VratiParceleSaPovrsinom(string imeVlasnika)
        {
            List<Tuple<Parcela, decimal>> parceleSaPovrsinom = new List<Tuple<Parcela, decimal>>();
            List<Parcela> parceleVlasnika;
            if (!_registrovaneParcele.TryGetValue(imeVlasnika, out parceleVlasnika))
                return null;

            foreach (var parcela in parceleVlasnika)
                parceleSaPovrsinom.Add(Tuple.Create(parcela, parcela.IzracunajPovrsinuMetriKvadratni()));

            return parceleSaPovrsinom;
        }

        public List<Parcela> VratiSveParcele()
        {
            return _registrovaneParcele.Values.SelectMany(parcela => parcela).ToList();
        }

        public List<Parcela> VratiVeceParcele(decimal povrsinaUArima)
        {
            return _registrovaneParcele.Values
                .SelectMany(parcela => parcela)
                .Where(parcela => parcela.IzracunajPovrsinuMetriKvadratni() / 100m > povrsinaUArima)
                .ToList();
        }
    }
}
